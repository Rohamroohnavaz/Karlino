using FinalProject_MVC.Models;
using FinalProject_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System.Reflection;

namespace FinalProject_MVC.Controllers
{
    [Authorize]
    [Route("Advertisements")]
    public class AdvertisementController : Controller
    {
        private readonly IApiService _apiService;
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementController(IApiService apiService 
            ,IAdvertisementRepository advertisementRepository)
        {
            _apiService = apiService;
            _advertisementRepository = advertisementRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            ViewData["Title"] = "آگهی‌ها";

            const int pageSize = 9;

            if (page < 1) page = 1;

            var (items, totalCount) = await _advertisementRepository.GetPagedForAdminAsync(
                search, true, page, pageSize);

            ViewBag.Search = search;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(items);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var email = User.Identity?.Name;

            var companyId = await _advertisementRepository.GetCompanyIdByUserEmailAsync(email);

            return View(new CreateAdvertisementViewModel
            {
                CompanyId = companyId ?? Guid.Empty
            });
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

        [AllowAnonymous]
        [HttpGet("/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var advertisement = await _apiService.GetAsync<AdvertisementViewModel>($"/GetAdvertisementById/{id}");

                if (advertisement == null)
                    return NotFound();

                advertisement.Id = id;

                ViewBag.IsOwner = await IsOwnerAsync(id);
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
            if (!await IsOwnerAsync(id))
            {
                return Forbid();
            }

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
            if (!await IsOwnerAsync(id))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var token = HttpContext.Session.GetString("Token");

                var existAdvertisement = await _apiService.GetAsync<AdvertisementViewModel>($"/GetAdvertisementById{id}");

                if(existAdvertisement == null)
                {
                    ModelState.AddModelError("", " (returned null) آگهی یافت نشد !");
                    return View(model);
                }

                var companyId = existAdvertisement.CompanyId;

                if(companyId == Guid.Empty)
                {
                    ModelState.AddModelError("", "آگهی خالی است !");
                    return View(model);
                }

                var advertisementData = new
                {
                    Id = id,
                    Title = model.Title,
                    Description = model.Description,
                    Salary = model.Salary,
                    CompanyName = model.CompanyName,
                    Province = model.Province,
                    City = model.City,
                    CompanyId = companyId
                };

                ViewBag.DebugInfo = $@"
                     Token: {(string.IsNullOrEmpty(token) ? "NULL ❌" : "Exists ✅")}
                     <br/>CompanyId from API: {companyId}
                     <br/>JSON: {Newtonsoft.Json.JsonConvert.SerializeObject(advertisementData)}
        ";

                await _apiService.PutAsync<object>("/UpdateAdvertisement", advertisementData);

                TempData["SuccessMessage"] = "آگهی با موفقیت آپدیت شد !";
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "خطا هنگام اپدیت آگهی : " + ex.Message);
                ViewBag.DebugInfo = $"Error Message : {ex.Message}";
                return View(model);
            }
        }

        [HttpGet("/Delete/{id}")]
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

        [HttpPost("/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (!await IsOwnerAsync(id))
            {
                return Forbid();  
            }

            try
            {
                await _apiService.PostAsync<object>($"/DeleteAdvertisement", new {Id = id});

                TempData["SuccessMessage"] = "آگهی با موفقیت حذف شد !";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "خطا هنگام حذف آگهی : " + ex.Message;
                var advertisement = await _apiService.GetAsync<AdvertisementViewModel>($"/GetAdvertisementById/{id}");
                return View("Delete", advertisement);
            }
        }

        [HttpGet("MyAds")]
        public async Task<IActionResult> MyAds()
        {
            ViewData["Title"] = "آگهی‌های من";

            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var model = await _advertisementRepository.GetMyAdsAsync(email);

            return View(model);
        }

        private async Task<bool> IsOwnerAsync(Guid id)
        {
            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
                return false;

            return await _advertisementRepository.IsOwnerAsync(id, email);
        }
    }
}
