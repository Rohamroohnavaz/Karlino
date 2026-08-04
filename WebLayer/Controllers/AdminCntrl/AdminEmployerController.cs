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
    [Route("api/admin/employer")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class AdminEmployerController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminEmployerController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployers()
        {
            var employers = await _adminService.GetEmployersAsync();
            return Ok(BaseResponseDto<List<AdminEmployerListDto>>.Success());
        }

        [HttpGet("/GetDetail/{employerId:guid}")]
        public async Task<IActionResult> GetEmployerDetail([FromRoute] Guid employerId)
        {
            var employerDetail = await _adminService.GetEmployerDetailsAsync(employerId);
            return Ok(BaseResponseDto<AdminEmployerDetailsDto>.Success());
        }

        [HttpPut("/approve_employer/{employerId:guid}")]
        public async Task<IActionResult> ApproveEmployer([FromBody] SendEmailRequest request
            , [FromRoute] Guid employerId
            , CancellationToken cancellationToken)
        {
            await _adminService.ApproveEmployerAsync(request, employerId, cancellationToken);
            return Ok(new { message = "Employer Successfuly Approved !!" });
        }

        [HttpPut("/reject_employer/{employerId:guid}")]
        public async Task<IActionResult> RejectEmployer([FromBody] SendEmailRequest request
            , [FromRoute] Guid employerId
            , CancellationToken cancellationToken)
        {
            await _adminService.RejectEmployersAsync(request, employerId, cancellationToken);
            return Ok(new { message = "Employer Successfuly Rejected !!"});
        }
    }
}
