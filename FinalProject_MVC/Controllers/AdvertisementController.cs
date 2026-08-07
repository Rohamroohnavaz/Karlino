using FinalProject_MVC.Models;
using FinalProject_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using System.Reflection;

namespace FinalProject_MVC.Controllers
{
    [Authorize]
    [Route("Advertisements")]
    public class AdvertisementController : Controller
    {
        private readonly IApiService _apiService;

        public AdvertisementController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
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

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdvertisementViewModel adverModel)
        {
            if(!ModelState.IsValid)
                return View(adverModel);

            try
            {
                var advertisementData = new
                {
                    adverModel.Title,
                    adverModel.Description,
                    adverModel.Salary,
                    adverModel.CompanyName,
                    adverModel.Province,
                    adverModel.City,
                    companyId = adverModel.CompanyId
                };

                await _apiService.PostAsync<object>("/CreateAdvertisement", advertisementData);

                TempData["SuccessMessage"] = "Advertisement Added Successfully !";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error In Adding Advertisement : " + ex.Message);
                return View(adverModel);
            }
        }

        [HttpGet("/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var advertisement = await _apiService.GetAsync<AdvertisementViewModel>($"/GetAdvertisementById/{id}");

                if (advertisement == null)
                    return NotFound();

                return View(advertisement);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "خطا هنگام دریافت جزئیات آگهی ! " + ex.Message;
                return View(new AdvertisementViewModel());
            }
        }
    }
}
