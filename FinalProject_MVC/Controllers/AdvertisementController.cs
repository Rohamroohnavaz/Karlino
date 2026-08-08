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
            if (!ModelState.IsValid)
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

                advertisement.Id = id;

                return View(advertisement);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "خطا هنگام دریافت جزئیات آگهی ! " + ex.Message;
                return View(new AdvertisementViewModel());
            }
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var advertisement = await _apiService.GetAsync<AdvertisementViewModel>($"/GetAdvertisementById/{id}");

                if (advertisement == null)
                    return NotFound();

                advertisement.Id = id;

                var companyIdFromClaim = User.FindFirst("CompanyId")?.Value;
                if (!string.IsNullOrEmpty(companyIdFromClaim) && Guid.TryParse(companyIdFromClaim, out var companyId))
                {
                    advertisement.CompanyId = companyId;
                }

                return View(advertisement);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "خطا در دریافت اطلاعات آگهی: " + ex.Message;
                return View(new AdvertisementViewModel());
            }
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AdvertisementViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var companyIdFromClaim = "019FCEB3-F887-7000-8189-B20F3DDC83EA";

                var advertisementData = new
                {
                    Id = id,
                    Title = model.Title,
                    Description = model.Description,
                    Salary = model.Salary,
                    CompanyName = model.CompanyName,
                    Province = model.Province,
                    City = model.City,
                    CompanyId = Guid.Parse(companyIdFromClaim)
                };

                await _apiService.PutAsync<object>("/UpdateAdvertisement", advertisementData);

                TempData["SuccessMessage"] = "آگهی با موفقیت آپدیت شد !";
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "خطا هنگام اپدیت آگهی : " + ex.Message);
                ViewBag.DebugInfo = ex.Message;
                return View(model);
            }
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var advertisement = await _apiService.GetAsync<AdvertisementViewModel>($"/GetAdvertisementById/{id}");

                if (advertisement == null)
                    return NotFound();

                advertisement.Id = id;

                return View(advertisement);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "خطا در دریافت اطلاعات آگهی: " + ex.Message;
                return View(new AdvertisementViewModel());
            }
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _apiService.PostAsync<object>($"/DeleteAdvertisement/{id}", null);

                TempData["SuccessMessage"] = "آگهی با موفقیت حذف شد !";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "خطا هنگام حذف آگهی : " + ex.Message;
                var advertisement = await _apiService.GetAsync<AdvertisementViewModel>($"/GetAdvertisementById/{id}");
                return View(advertisement);
            }
        }
    }
}
