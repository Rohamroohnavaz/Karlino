using FinalProject_MVC.Areas.JobSeeker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;

namespace FinalProject_MVC.Areas.JobSeeker.Controllers
{
    [Area("JobSeeker")]
    [Authorize(Roles = RoleConstants.JobSeekerRole)]
    public class JobsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAdvertisementService _advertisementService;
        private readonly IRequestResumeService _requestResumeService;

        public JobsController(UserManager<User> userManager
            , IAdvertisementService advertisementService
            , IRequestResumeService requestResumeService)
        {
            _userManager = userManager;
            _advertisementService = advertisementService;
            _requestResumeService = requestResumeService;
        }

        public async Task<IActionResult> Index(string jobTitle, string city)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var advertisements = await _advertisementService.GetActiveAdvertisementsAsync(jobTitle, city);
            var userApplications = await _advertisementService.GetUserAppliedAdvertisementIdsAsync(user.Id);

            var viewModel = new JobSearchViewModel
            {
                JobTitle = jobTitle,
                City = city,
                Jobs = advertisements.Select(a => new JobsViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    CompanyName = a.Company?.CompanyName ?? "نامشخص",
                    City = a.City ?? "",
                    Salary = a.Salary.ToString() ?? "توافقی",
                    PostedDate = a.CreatedAt,
                    Description = a.Description ?? "",
                    HasApplied = userApplications.Contains(a.Id)
                }).ToList()
            };

            ViewData["Title"] = "جستجوی شغل";
            return View(viewModel);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var advertisement = await _advertisementService.GetAdvertisementWithDetailsAsync(id);
            if (advertisement == null) return NotFound();

            var hasApplied = await _advertisementService.UserHasAppliedAsync(user.Id, id);

            var viewModel = new JobsViewModel
            {
                Id = advertisement.Id,
                Title = advertisement.Title,
                CompanyName = advertisement.Company?.CompanyName ?? "نامشخص",
                City = advertisement.City ?? "",
                Salary = advertisement.Salary.ToString() ?? "توافقی",
                PostedDate = advertisement.CreatedAt,
                Description = advertisement.Description ?? "",
                HasApplied = hasApplied
            };

            ViewData["Title"] = advertisement.Title;
            return View(viewModel);
        }

        // POST: Jobs/Apply
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(Guid advertisementId)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null)
            {
                TempData["ErrorMessage"] = "کاربر یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            var success = await _requestResumeService.ApplyForAdvertisementAsync(user.Id, advertisementId);

            if (!success)
            {
                TempData["ErrorMessage"] = "خطا در ارسال درخواست. لطفاً مطمئن شوید رزومه شما تکمیل است.";
                return RedirectToAction(nameof(Details), new { id = advertisementId });
            }

            TempData["SuccessMessage"] = "درخواست شما با موفقیت ارسال شد!";
            return RedirectToAction(nameof(Details), new { id = advertisementId });
        }

        //private async Task<User> GetUserAsync()
        //{
        //    var userEmail = User.Identity?.Name;
        //    return await _userManager.FindByEmailAsync(userEmail);
        //}
    }
}