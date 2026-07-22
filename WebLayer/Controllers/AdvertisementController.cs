using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("api/")]
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

        [HttpPost("CreateAdvertisement")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CreateAdver([FromBody]CreateAdvertisementDto dto)
        { 
               await _advertisementService.CreateAdvertisement(dto);
               return Ok();
        }

        //[HttpGet("/GetAdvertisement")]
        //public async Task<IActionResult> GetAdvertisementByCompanyId([FromRoute] Guid companyId)
        //{
        //    var 
        //}
    }
}
