using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFinalProject.Application.Commands;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.Results;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public AuthenticationService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ICompanyService companyService
            ,JwtSettings jwtSettings
            ,IUnitOfWork unitOfWork
            ,ICompanyRepository companyRepository
            ,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _companyService = companyService;
            _jwtSettings = jwtSettings;
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
            _configuration = configuration;
        }

        public async Task<RegisterResult> RegisterEmployerAsync(RegisterEmployerCommand command)
        {
            var findEmployer = await _userManager.FindByNameAsync(command.Username);

            if (findEmployer != null)
                throw new PermissionDeniedException();

            var user = new User(command.Firstname, command.Lastname, command.Phonenumber, command.Email);

            user.UserName = command.Username;
            user.IsApproved = false;
            user.Role = UserRole.Employer;

            var createResult = await _userManager.CreateAsync(user, command.Password);
            if (!createResult.Succeeded)
                throw new RegistrationUserException(string.Join(" | ", createResult.Errors.Select(x => x.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.EmployerRole);
            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new RegistrationUserException("! Unsuccessfuly AddRole !");
            }

            var company = new Company(command.CompanyName, command.CompanyLocation,
                command.Province, command.City ,user.Id);

            await _companyRepository.AddAsync(company);
            await _unitOfWork.SaveChangesAsync();

            return new RegisterResult
            {
                IsSuccess = true,
                Message = "Registration Successfull ."
            };
        }

        public async Task<RegisterResult> RegisterJobSeekerAsync(RegisterJobSeekerCommand command)
        {
            var findEmployer = await _userManager.FindByNameAsync(command.Username);

            if (findEmployer != null)
                throw new PermissionDeniedException();

            var user = new User(command.Firstname, command.Lastname, command.Phonenumber, command.Email);

            user.UserName = command.Username;
            user.IsApproved = false;
            user.Role = UserRole.JobSeeker;

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
                IsSuccess = true,
                Message = "Registration Successfull ."
            };
        }

        public async Task<LoginResult> LoginAsync(LoginUserCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.Username);
            if (user == null)
                throw new UserNotFoundException("User not found !");

            if (!await _userManager.CheckPasswordAsync(user, command.Password))
                throw new PermissionDeniedException();

            if (!user.IsApproved)
                throw new PermissionDeniedException();

            var token = await GenerateTokenAsync(user);

            return new LoginResult
            {
                IsSuccess = true,
                MainToken = token,
                Username = user.UserName!,
                Role = user.Role.ToString()
            };
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            if (!user.IsApproved)
                throw new PermissionDeniedException();

            var jwtSection = _configuration.GetSection("JwtSettings");
            var key = jwtSection["Key"]!;
            var issuer = jwtSection["Issuer"]!;
            var audience = jwtSection["Audience"]!;
            var expiryMinutes = int.Parse(jwtSection["DurationInMinutes"]!);

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim(ClaimConstants.Username, user.UserName ?? string.Empty),
               new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
               new Claim(ClaimConstants.Role, user.Role.ToString())
            };

            if (user.Role == UserRole.Employer && user.Company != null)
            {
                claims.Add(new Claim("CompanyId", user.Company.Id.ToString()));
            }

            var roleName = user.Role.ToString();
            claims.Add(new Claim(ClaimTypes.Role, roleName));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); 
        }
    }
}
