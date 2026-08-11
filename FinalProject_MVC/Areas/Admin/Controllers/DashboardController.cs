using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Infrastructure;
using System.Threading.Tasks;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    public class DashboardController : AdminBaseController
    {
        private readonly FinalDbContext _dbContext;

        public DashboardController(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //ViewData["Title"] = " داشبورد مدیریت";

            return Content("Admin Dashboard Works");

            //var model = new AdminDashboardViewModel
            //{
            //    TotalUsers = await _dbContext.Users.CountAsync(),
            //    TotalEmployers = await _dbContext.Companies.CountAsync(),
            //};

            //return View();
        }
    }
}
