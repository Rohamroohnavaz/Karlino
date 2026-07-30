using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs.AdminDTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using WebLayer.Models;

namespace WebLayer.Controllers.AdminCntrl
{
    [ApiController]
    [Route("api/admin/advertisement")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class AdminAdvertisementController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminAdvertisementController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdvertisements()
        {
            var advers = await _adminService.GetAllAdvertisementAsync();
            return Ok(BaseResponseDto<List<AdminAdvertisementListDto>>.Success(advers));
        }

        [HttpPut("/{adverId:guid}/feature")]
        public async Task<IActionResult> FeatureAdvertisements([FromRoute] Guid adverId
            ,[FromBody] bool isFeatured,[FromQuery] int days)
        {
            await _adminService.FeatureAdvertisementAsync(adverId, isFeatured, days);
            return Ok(ResponseDto.Success());
        }

        [HttpPut("{adverId:guid}/unfeature")]
        public async Task<IActionResult> UnfeatureAdvertisement([FromRoute] Guid adverId)
        {
            await _adminService.FeatureAdvertisementAsync(adverId, false, 0);
            return Ok(ResponseDto.Success());
        }

        [HttpPut("{adverId:guid}/activate")]
        public async Task<IActionResult> Activate([FromRoute] Guid adverId)
        {
            await _adminService.ToggleAdvertisementStatusAsync(adverId, true);
            return Ok(ResponseDto.Success());
        }

        [HttpPut("{adverId:guid}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid adverId)
        {
            await _adminService.ToggleAdvertisementStatusAsync(adverId, false);
            return Ok(ResponseDto.Success());
        }

        [HttpDelete("{adverId:guid}/softdelete")]
        public async Task<IActionResult> SoftDeleteAdvertisement([FromRoute] Guid adverId)
        {
            await _adminService.SoftDeleteAdvertisementAsync(adverId);
            return Ok(ResponseDto.Success());
        }
    }
}
