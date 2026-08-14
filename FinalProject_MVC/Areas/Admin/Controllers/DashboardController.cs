using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Infrastructure;
using System.Threading.Tasks;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class DashboardController : AdminBaseController
    {
        private readonly IAdminService _adminService;

        public DashboardController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = " داشبورد مدیریت";

            var model = await _adminService.GetDashboardStatsAsync();

            return View();
        }
    }
}
