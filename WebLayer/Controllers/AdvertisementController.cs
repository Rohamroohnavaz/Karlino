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
        public async Task<IActionResult> CreateAdver([FromBody] CreateAdvertisementDto dto)
        {
            try
            {
                //var advertisement = new Advertisement(dto.Title, dto.Description ,dto.Salary ,dto.);
                await _advertisementService.CreateAdvertisement(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex);
            }

            return Created();
        }
    }
}
