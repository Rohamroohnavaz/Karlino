using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Services.ServiceInterfaces;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private readonly IAdminService _adminService;

        public UsersController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "مدیریت کاربران";

            var model = await _adminService.GetAllUsersAsync();

            return View(model);
        }
    }
}