using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Commands;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Results;
using MyFinalProject.Application.Services.MainServices.AuthServices;
using System.Diagnostics.Eventing.Reader;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebLayer.Models;
using IAuthenticationService = MyFinalProject.Application.Services.MainServices.AuthServices.IAuthenticationService;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("/RegisterEmployer")]
        public async Task<IActionResult> RegisterEmployer([FromBody] RegisterEmployerCommand command)
        {
            var employerResult = await _authenticationService.RegisterEmployerAsync(command);
            return Ok(BaseResponseDto<RegisterResult>.Success());
        }

        [AllowAnonymous]
        [HttpPost("/RegisterJobSeeker")]
        public async Task<IActionResult> RegisterJobSeeker([FromBody] RegisterJobSeekerCommand command)
        {
            var jobSeekerResult = await _authenticationService.RegisterJobSeekerAsync(command);
            return Ok(BaseResponseDto<RegisterResult>.Success());
        }

        [AllowAnonymous]
        [HttpPost("/LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command)
        {
            var loginResult = await _authenticationService.LoginAsync(command);
            return Ok(BaseResponseDto<LoginResultForRefresh>.Success());
        }

        [Authorize]
        [HttpPost("/LogoutUser")]
        public async Task<IActionResult> LogoutUser()
        {
            var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
            var expire = User.FindFirstValue(JwtRegisteredClaimNames.Exp);

            if (string.IsNullOrWhiteSpace(jti) || string.IsNullOrWhiteSpace(expire)
                || long.TryParse(expire, out var expireTime))
                return BadRequest("Invalid Token !");

            var expiresAtUtc = DateTimeOffset.FromUnixTimeSeconds(expireTime).UtcDateTime;

            await _authenticationService.LogoutAsync(jti , expiresAtUtc);
            return NoContent();
        }
    }
}
