using FinalProject_MVC.Models;
using FinalProject_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalProject_MVC.Controllers
{
    public class AdvertisementMvcController : Controller
    {
        private readonly IApiService _apiService;

        public AdvertisementMvcController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var advertisement = await _apiService.GetAsync<List<AdvertisementViewModel>>("/Advertisements");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage =  "Getting Advertisements Failed !!" + ex.Message;
                return View(new List<AdvertisementViewModel>());
            }
        }
    }
}
