using FinalProject_MVC.Models;
using FinalProject_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject_MVC.Controllers
{
    [Route("Advertisements")]
    public class AdvertisementController : Controller
    {
        private readonly IApiService _apiService;

        public AdvertisementController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var advertisements = await _apiService.GetAsync<List<AdvertisementViewModel>>("/GetActiveAdvertisements");
                return View(advertisements);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Getting Advertisement Failed !!" + ex.Message;
                return View(new List<AdvertisementViewModel>());
            }
        }
    }
}
