using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using WebLayer.Models;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/RequestResume")]
    [Authorize]
    public class RequestResumeController : ControllerBase
    {
        private readonly IRequestResumeService _requestResumeService;

        public RequestResumeController(IRequestResumeService requestResumeService)
        {
            _requestResumeService = requestResumeService;
        }

        [HttpPut("/ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromBody]ChangeRequestStatusDto dto)
        {
            await _requestResumeService.ChangeRequestStatusAsync(dto);
            return Ok(ResponseDto.Success());
        }

        [HttpGet("/{advertisementId:guid}")]
        public async Task<IActionResult> GetRequestsAsync([FromRoute]Guid advertisementId)
        {
            var result = await _requestResumeService.GetRequestsByAdverIdAsync(advertisementId);
            return Ok(BaseResponseDto<List<RequestResume>>.Success());
        }

        [HttpPost("/CreateResume")]
        [Authorize(Roles = RoleConstants.JobSeekerRole)]
        public async Task<IActionResult> CreateRequestResumeAsync([FromBody] CreateRequestResumeDto dto ,Guid adverId)
        {
            await _requestResumeService.CreateResumeRequest(adverId, dto);
            return Ok(ResponseDto.Success());
        }

        [HttpGet("/GetRequests/{adverId:guid}")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> GetRequestByAdverId([FromRoute] Guid adverId)
        {
            var request = await _requestResumeService.GetRequestsByAdverIdAsync(adverId);

            if(request is null)
                return NotFound();

            return Ok(BaseResponseDto<List<RequestResume>>.Success());
        }

        [HttpPost("/UploadFile")]
        public async Task<IActionResult> UploadAttachFile([FromBody] UploadAttachDto dto,
            [FromRoute] Guid requestId)
        {
            await _requestResumeService.UploadFileAttachAsync(requestId ,dto);
            return Ok(ResponseDto.Success());
        }

        [HttpPost("/ReplaceFile")]
        public async Task<IActionResult> ReplaceAttachFile([FromBody] UploadAttachDto dto,
            [FromRoute] Guid requestId)
        {
            await _requestResumeService.ReplaceFileAttachAsync(requestId ,dto);
            return Ok(ResponseDto.Success());
        }

    }
}
