using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs.AdminDTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using WebLayer.Models;

namespace WebLayer.Controllers.AdminCntrl
{
    [ApiController]
    [Route("api/admin/dashboard")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminDashboardController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = await _adminService.GetDashboardStatsAsync();
            return Ok(BaseResponseDto<AdminDashboardDto>.Success());
        }
    }
}
