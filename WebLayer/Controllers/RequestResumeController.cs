using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/requestresume")]
    public class RequestResumeController : ControllerBase
    {
        private readonly IRequestResumeService _requestResumeService;

        public RequestResumeController(IRequestResumeService requestResumeService)
        {
            _requestResumeService = requestResumeService;
        }

        [HttpPut("api/change")]
        public async Task<IActionResult> ChangeStatus([FromBody]ChangeRequestStatusDto dto)
        {
            await _requestResumeService.ChangeRequestStatusAsync(dto);

            return Ok();
        }

    }
}
