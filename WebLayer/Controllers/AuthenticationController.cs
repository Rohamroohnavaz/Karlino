using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.MainServices.AuthServices;
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

        [HttpPost("api/RegisterEmployer")]
        public async Task<IActionResult> AddEmployer([FromBody]AddEmployerRequestDto dto)
        {
            var result = await _authenticationService.RegisterEmployerAsync(dto.ToCommand());
            return Ok(result);
        }

        [HttpPost("api/RegisterJobSeeker")]
        public async Task<IActionResult> AddJobSeeker([FromBody]AddEmployerRequestDto dto)
        {
            var result = await _authenticationService.RegisterEmployerAsync(dto.ToCommand());
            return Ok(result);
        }


    }
}
