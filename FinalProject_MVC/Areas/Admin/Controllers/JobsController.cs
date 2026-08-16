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

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "مدیریت آگهی ها";

            var model = await _adminService.GetLatestJobPostingsAsync(10);

            return View(model);
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
