using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Constants;
using MyFinalProject.Infrastructure;
using System.Threading.Tasks;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index()
        {
            ViewData["Title"] = " داشبورد مدیریت";

            return View();

            //return Content("Admin Dashboard Worked !");

            //var model = new AdminDashboardViewModel
            //{
            //    TotalUsers = await _dbContext.Users.CountAsync(),
            //    TotalEmployers = await _dbContext.Companies.CountAsync(),
            //};
        }
    }
}
