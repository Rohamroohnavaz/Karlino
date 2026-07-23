using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using System.Diagnostics;
using WebLayer.Models;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/GetProfile")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> GetJobSeekerProfile([FromRoute] Guid userId)
        {
            var user = await _userService.GetJobSeekerProfile(userId);

            if (user == null)
                return BadRequest(new ResponseDto("user not found","404"));
            return Ok(user);
        } 

        [HttpPut("/UpdateProfile")]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
        {
            await _userService.UpdateProfileUser(dto);
            return Ok();
        }
    }
}
