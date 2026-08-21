using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFinalProject.Application.Commands;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Results;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Application.Services.Settings;
using MyFinalProject.Domain.Entities;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace MyFinalProject.Application.Services.MainServices.AuthServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ICompanyService _companyService;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly FinalDbContext _context;

        public AuthenticationService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ICompanyService companyService
            , IOptions<JwtSettings> jwtOptions
            , IUnitOfWork unitOfWork
            , ICompanyRepository companyRepository
            , IConfiguration configuration
            , IRefreshTokenRepository refreshTokenRepository
            , FinalDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _companyService = companyService;
            _jwtSettings = jwtOptions.Value;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _context = context;
        }

        public async Task<RegisterResult> RegisterEmployerAsync(RegisterEmployerCommand command)
        {
            var findEmployer = await _userManager.FindByNameAsync(command.Email.Trim());

            if (findEmployer != null)
                throw new DuplicateUserException("Duplicate Key");

            if (string.IsNullOrWhiteSpace(command.Email))
                throw new ArgumentException("Email is required !");

            if (string.IsNullOrWhiteSpace(command.Password))
                throw new ArgumentException("Password is required !");

            var user = new User(command.Firstname, command.Lastname, command.Phonenumber,
                command.Email)
            {
                UserName = command.Email,
                IsApproved = false,
                Role = UserRole.Employer
            };

            var createResult = await _userManager.CreateAsync(user, command.Password);
            if (!createResult.Succeeded)
                throw new RegistrationUserException(string.Join(" | ", createResult.Errors.Select(x => x.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.EmployerRole);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new RegistrationUserException("! Unsuccessfuly AddRole !");
            }

            try
            {
                var company = new Company(command.CompanyName, command.CompanyLocation,
                command.Province, command.City, user.Id);

                await _companyRepository.AddAsync(company);
                await _unitOfWork.SaveChangesAsync();

                return new RegisterResult
                {
                    IsSuccess = true,
                    Message = "Employer Registered Successfully !"
                };
            }
            catch
            {
                await _userManager.RemoveFromRoleAsync(user, RoleConstants.EmployerRole);
                await _userManager.DeleteAsync(user);
                throw;
            }
        }

        public async Task<RegisterResult> RegisterJobSeekerAsync(RegisterJobSeekerCommand command)
        {
            var findEmployer = await _userManager.FindByNameAsync(command.Email.Trim());

            if (findEmployer != null)
                throw new DuplicateUserException("A JobSeeker With This Username Is Exist !");

            if (string.IsNullOrWhiteSpace(command.Email))
                throw new ArgumentException("Email is required !!");

            if (string.IsNullOrWhiteSpace(command.Password))
                throw new ArgumentException("Password is required !!");

            var user = new User(command.Firstname, command.Lastname, command.Phonenumber, command.Email)
            {
                UserName = command.Email,
                Role = UserRole.JobSeeker,
                IsApproved = false
            };

            var createResult = await _userManager.CreateAsync(user, command.Password);
            if (!createResult.Succeeded)
                throw new RegistrationUserException(string.Join(" | ", createResult.Errors.Select(x => x.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.JobSeekerRole);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new RegistrationUserException("! Unsuccessfuly Add Role !");
            }

            return new RegisterResult
            {
                ResultId = user.Id,
                IsSuccess = true,
                Message = "Registration Successfull ."
            };
        }

        public async Task<LoginResultForRefresh> LoginAsync(LoginUserCommand command)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
                throw new UserNotFoundException("User not found !!");

            var passwordValid = await _userManager.CheckPasswordAsync(user, command.Password);
            if (!passwordValid)
                throw new UnauthorizedAccessException("Invalid Email Or Password");

            if (!user.IsApproved)
                throw new EnforceApproveException("User is not approved!!");

            var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);
            var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Role, user.Role.ToString()),
               new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString())
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessTokenExpiresAt,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString("N");

            await _refreshTokenRepository.AddAsync(new RefreshToken(refreshToken,
               user.Id, refreshTokenExpiresAt)
            {
                IsRevoked = false
            });

            await _unitOfWork.SaveChangesAsync();

            return new LoginResultForRefresh
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshTokenExpiresAt = refreshTokenExpiresAt
            };
        }

        public async Task<GenerateTokenResult> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
               new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
               new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            if (user.Role == UserRole.Employer)
            {
                if (user.CompanyId.HasValue)
                {
                    claims.Add(new Claim("CompanyId", user.CompanyId.Value.ToString()));
                }
            }

            var userRoles = (await _userManager.GetRolesAsync(user))
                .Select(r => new Claim(ClaimTypes.Role, r))
                .ToList();

            foreach (var claim in userRoles)
            {
                var role = _roleManager.Roles
                    .FirstOrDefault(r => r.Name == claim.Value);

                if (role is null)
                    continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            claims.AddRange(userRoles);

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);


            var secretKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var expiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var credentials = new SigningCredentials(
               secretKey,
               SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expiresIn,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            var expiresInSeconds = expiresIn.Subtract(DateTime.UtcNow).TotalSeconds;

            return new GenerateTokenResult(accessToken, expiresInSeconds);
        }

        public async Task LogoutAsync(string jti, DateTime expiresAtUtc)
        {
            if (string.IsNullOrWhiteSpace(jti))
                throw new ArgumentException("Jti is invalid !!");

            var revoked = await _context.RevokedTokens
                .FirstOrDefaultAsync(r => r.Jti == jti);

            if (revoked != null)
                return;

            await _context.RevokedTokens.AddAsync(new RevokedToken
            {
                RevokeId = Guid.NewGuid(),
                Jti = jti,
                ExpiresAtUtc = expiresAtUtc,
                RevokedAtUtc = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<LoginResultForRefresh> RefreshTokenAsync(RefreshTokenRequestDto dto,
          CancellationToken cancellationToken = default)
        {
            var storedToken = await _refreshTokenRepository
                .GetTokenWithUserAsync(dto.RefreshToken, cancellationToken);

            if (storedToken is null)
                throw new UnauthorizedAccessException("Invalid refresh token!");

            if (storedToken.IsRevoked)
            {
                await _refreshTokenRepository.RevokeAllUserTokensAsync(
                    storedToken.UserId,
                    "Refresh token reuse detected",
                    cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                throw new SecurityException("Refresh token reuse detected.");
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
                throw new UnauthorizedAccessException("Refresh token expired.");

            var newRefreshToken = new RefreshToken(Guid.NewGuid().ToString(),
                storedToken.UserId, DateTime.UtcNow.AddDays(7));


            await _refreshTokenRepository.RevokeAsync(
                storedToken,
                replacedByToken: newRefreshToken.Token,
                revokeReason: "Rotated",
                cancellationToken: cancellationToken);

            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);

            var accessToken = await GenerateTokenAsync(storedToken.User);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoginResultForRefresh
            {
                AccessToken = accessToken.AccessToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiresAt = newRefreshToken.ExpiresAt
            };
        }

        public async Task ChangeRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException($"{nameof(user)} not found !!");

            var currentRole = await _userManager.GetRolesAsync(user);
            if (currentRole.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRole);

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
                throw new InvalidOperationException();

            await _userManager.AddToRoleAsync(user, roleName);

            if(Enum.TryParse<UserRole>(roleName, out var userRole))
            {
                user.Role = userRole;
                await _userManager.UpdateAsync(user);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
