using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Services.ServiceInterfaces;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    public class EmployersController : AdminBaseController
    {
        private readonly IAdminService _adminService;

        public EmployersController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "کارفرماهای در انتظار تأیید";

            var model = await _adminService.GetPendingEmployersAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _adminService.ApproveEmployerAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Guid id)
        {
            await _adminService.RejectEmployerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}