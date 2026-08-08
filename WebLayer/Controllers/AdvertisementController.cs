using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Commands.AdverCommands;
using MyFinalProject.Application.Commands.ViewModels;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Filters;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using WebLayer.Models;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/adv")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly ICurrentUserService _currentUserService;

        public AdvertisementController(IAdvertisementService advertisementService
            , ICurrentUserService currentUserService)
        {
            _advertisementService = advertisementService;
            _currentUserService = currentUserService;
        }

        [HttpPost("/CreateAdvertisement")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> CreateAdver([FromBody] CreateAdvertisementDto dto)
        {
            await _advertisementService.CreateAdvertisementAsync(dto);
            return Ok(ResponseDto.Success());
        }

        [HttpGet("/GetActiveAdvertisements")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActiveAdvers()
        {
            var advers = await _advertisementService.GetActiveAdvertisementAsync();
            return Ok(advers);
        }

        [HttpGet("/SearchAdvertisement")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAdvertisement([FromBody] AdverSearchFilterDto filter)
        {
            var result = await _advertisementService.SearchAndFilterAdsAsync(filter);
            return Ok(BaseResponseDto<List<AdvertisementViewModel>>.Success());
        }

        [HttpGet("/GetAdvertisementByCompanyId/{companyId:guid}")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> GetAdvertisementByCompanyId(Guid companyId)
        {
            var adver = await _advertisementService.GetAdvertisementByCompanyIdAsync(companyId);
            return Ok(adver);
        }

        [HttpGet("/GetAdvertisementById/{id:guid}")]
        public async Task<IActionResult> GetAdvertisementById(Guid id)
        {
            var adver = await _advertisementService.GetAdvertisementByIdAsync(id);
            return Ok(adver);
        }

        [HttpPut("/UpdateAdvertisement")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> UpdateAdvertisement([FromBody] UpdateAdvertisementCommand command)
        {
            try
            {
                await _advertisementService.UpdateAdvertisement(command);
                return Ok(new { message = "Advertisement Updated Successfully !" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("/DeleteAdvertisement")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> DeleteAdvertisement([FromBody] DeleteAdvertisementCommand command)
        {
            try
            {
                await _advertisementService.DeleteAdvertisement(command);
                return Ok(new {message = "Advertisement Deleted Successfully !"});
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
