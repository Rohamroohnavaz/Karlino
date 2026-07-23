using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Commands;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.MainServices.AuthServices;
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

        [HttpPost("/RegisterEmployer")]
        public async Task<IActionResult> RegisterEmployer([FromBody] RegisterEmployerCommand command)
        {
            var employerResult = await _authenticationService.RegisterEmployerAsync(command);
            return Ok(employerResult);
        }

        [HttpPost("/RegisterJobSeeker")]
        public async Task<IActionResult> RegisterJobSeeker([FromBody] RegisterJobSeekerCommand command)
        {
            var jobSeekerResult = await _authenticationService.RegisterJobSeekerAsync(command);
            return Ok(jobSeekerResult);
        }

        [HttpPost("/LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command)
        {
            var loginResult = await _authenticationService.LoginAsync(command);
            return Ok(loginResult);
        }
    }
}
