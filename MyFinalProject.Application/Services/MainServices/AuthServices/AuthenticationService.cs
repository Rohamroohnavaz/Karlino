using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFinalProject.Application.Commands;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.Results;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
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
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterUserCommand command)
        {
            var findUser = await _userManager.FindByNameAsync(command.Firstname);

            if (findUser != null)
                throw new DuplicateUserException(command.Firstname);

            var user = new User(command.Firstname, command.Lastname, command.Phonenumber, command.Email);

            var result = await _userManager.CreateAsync(user, command.Password);
            var roleManaging = await _userManager.AddToRoleAsync(user, RoleConstants.EmployerRole);
            
            if (!result.Succeeded)
                throw new RegistrationUserException(result.Errors.FirstOrDefault()?.Description ?? "Registration Failed !!");

            return new RegisterResult(user.Id);
        }

        public async Task<LoginResult> LoginAsync(LoginUserCommand command)
        {
            var result = await _signInManager
                .PasswordSignInAsync(command.Username, command.Password, false, true);

            if (result.IsLockedOut)
                throw new AuthenticationException("User is locked out. Please try again 15 minutes later.");

            if (result.IsNotAllowed)
                throw new PermissionDeniedException("Invalid Password !!");

            if (!result.Succeeded)
                throw new AuthenticationException("Invalid username or password.");

            var user = await _userManager.FindByNameAsync(command.Username);

            if (user is null)
                throw new UserNotFoundException(command.Username);

            return await GenerateTokenAsync(user);
        }

        public async Task<LoginResult> GenerateTokenAsync(User user)
        {
            var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim("CompanyId" , user.Id.ToString())
        };

            var userRoles = (await _userManager.GetRolesAsync(user))
                .Select(r => new Claim(ClaimTypes.Role, r)).ToList();

            foreach (var claim in userRoles)
            {
                var role = _roleManager.Roles.FirstOrDefault(r => r.Name == claim.Value);

                if (role is null)
                    continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            claims.AddRange(userRoles);

            var userClaims = await _userManager.GetClaimsAsync(user);

            claims.AddRange(userClaims);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expiresIn,
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token)!;
            var expiresInSeconds = expiresIn.Subtract(DateTime.UtcNow).TotalSeconds;
            return new LoginResult(accessToken, expiresInSeconds);
        }
    }
}
