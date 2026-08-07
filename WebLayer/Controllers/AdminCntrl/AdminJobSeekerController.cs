using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs.AdminDTOs;
using MyFinalProject.Application.Requests;
using MyFinalProject.Application.Services.ServiceInterfaces;
using WebLayer.Models;

namespace WebLayer.Controllers.AdminCntrl
{
    [ApiController]
    [Route("api/admin/jobSeeker")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class AdminJobSeekerController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminJobSeekerController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobSeekers()
        {
            var jobSeekers = await _adminService.GetJobSeekersAsync();
            return Ok(BaseResponseDto<List<AdminJobSeekerListDto>>.Success());
        }

        [HttpGet("/{jobSeekerId:guid}/detail")]
        public async Task<IActionResult> GetJobSeekerDetail([FromRoute] Guid jobSeekerId)
        {
            var jobSeeker = await _adminService.GetJobSeekerDetailsAsync(jobSeekerId);
            return Ok(BaseResponseDto<AdminJobSeekerDetailsDto>.Success());
        }

        [HttpPut("/{jobSeekerId:guid}/approve_jobSeeker")]
        public async Task<IActionResult> ApproveJobSeeker([FromBody] SendEmailRequest request 
            ,[FromRoute] Guid jobSeekerId 
            ,CancellationToken cancellationToken)
        {
            var jobSeeker = await _adminService.ApproveJobSeekerAsync(request, jobSeekerId, cancellationToken);
            return Ok(new { message = "JobSeeker Successfuly Approved !!"} );
        }

        [HttpPut("/{jobSeekerId:guid}/reject_jobSeeker")]
        public async Task<IActionResult> RejectJobSeeker([FromBody] SendEmailRequest request 
            ,[FromRoute] Guid jobSeekerId 
            ,CancellationToken cancellationToken)
        {
            var jobSeeker = await _adminService.RejectJobSeekerAsync(request, jobSeekerId, cancellationToken);
            return Ok(new { message = "JobSeeker Successfuly Rejected !!"});
        }

        [HttpPut("{jobSeekerId:guid}/activate")]
        public async Task<IActionResult> Activate([FromRoute] Guid jobSeekerId)
        {
            await _adminService.ToggleJobSeekerStatusAsync(jobSeekerId, true);
            return Ok(ResponseDto.Success());
        }

        [HttpPut("{jobSeekerId:guid}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid jobSeekerId)
        {
            await _adminService.ToggleJobSeekerStatusAsync(jobSeekerId, false);
            return Ok(ResponseDto.Success());
        }
    }
}
