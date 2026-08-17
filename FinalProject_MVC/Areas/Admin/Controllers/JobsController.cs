using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Services.ServiceInterfaces;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    public class JobsController : AdminBaseController
    {
        private readonly IAdminService _adminService;

        public JobsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index(string? search, bool? isActive, int page = 1)
        {
            ViewData["Title"] = "مدیریت آگهی‌ها";

            const int pageSize = 10;

            if (page < 1) page = 1;

            var (items, totalCount) = await _adminService.GetPagedJobPostingsAsync(
                search, isActive, page, pageSize);

            ViewBag.Search = search;
            ViewBag.IsActive = isActive;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _adminService.SetJobPostingActiveAsync(id, true);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Guid id)
        {
            await _adminService.SetJobPostingActiveAsync(id, false);
            return RedirectToAction(nameof(Index));
        }
    }
}
