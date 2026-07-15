using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyFinalProject.Application.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices.AuthServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public Task GenerateToken()
        //{
        //    var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.NameIdentifier ,appUser.Id),
        //        new Claim(ClaimTypes.Name ,appUser.FullName),

        //        new (JwtRegisteredClaimNames.Sub ,appUser.Id),
        //        new (JwtRegisteredClaimNames.Email ,appUser.Email),
        //        new (AppClaims.IsApproved ,appUser.IsApproved.ToString()),
        //        new (AppClaims.IsPro ,appUser.IsPro.ToString()),

        //    };


        //    foreach (var role in Roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["Jwt:Issuer"],
        //        audience: _configuration["Jwt:Audience"],
        //        expires: DateTime.UtcNow.AddHours(2),
        //        signingCredentials: new SigningCredentials(
        //            new SymmetricSecurityKey(
        //                System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
        //                , SecurityAlgorithms.HmacSha256)
        //        );
        //}
    }
}
