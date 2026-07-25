using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Filters;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/adv")]
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
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CreateAdver([FromBody]CreateAdvertisementDto dto)
        { 
               await _advertisementService.CreateAdvertisementAsync(dto);
               return Ok();
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
            return Ok(result);
        }
    }
}
