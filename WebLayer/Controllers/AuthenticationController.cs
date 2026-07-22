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

        [HttpPost("api/Register")]
        public async Task<IActionResult> AddUserAsync([FromBody]AddUserRequestDto dto)
        {
            var result = await _authenticationService.RegisterAsync(dto.ToCommand());
            return Ok(result);
        }


    }
}
