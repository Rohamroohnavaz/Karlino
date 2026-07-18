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

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpPost("CreateAdvertisement")]
        public async Task<IActionResult> CreateAdver([FromBody]CreateAdvertisementDto dto)
        { 
               var result = await _advertisementService.CreateAdvertisement(dto);
               return Ok(result);
        }
    }
}
