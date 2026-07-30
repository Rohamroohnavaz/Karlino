using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly ICurrentUserService _currentUserService;

        public AdvertisementController(IAdvertisementService advertisementService
            ,ICurrentUserService currentUserService)
        {
            _advertisementService = advertisementService;
            _currentUserService = currentUserService;
        }

        [HttpPost("/CreateAdvertisement")]
        [Authorize(Roles = RoleConstants.EmployerRole)]
        public async Task<IActionResult> CreateAdver([FromBody]CreateAdvertisementDto dto)
        { 
               await _advertisementService.CreateAdvertisementAsync(dto);
               return Ok(ResponseDto.Success());
        }

        [HttpGet("/GetActiveAdvertisements")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActiveAdvers()
        {
            var advers = await _advertisementService.GetActiveAdvertisementAsync();
            return Ok(BaseResponseDto<List<AdvertisementViewModel>>.Success());
        }

        [HttpGet("/SearchAdvertisement")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAdvertisement([FromBody] AdverSearchFilterDto filter)
        {
            var result = await _advertisementService.SearchAndFilterAdsAsync(filter); 
            return Ok(BaseResponseDto<List<AdvertisementViewModel>>.Success());
        }
    }
}
