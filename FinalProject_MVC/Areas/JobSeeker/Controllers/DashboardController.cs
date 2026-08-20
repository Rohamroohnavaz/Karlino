using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;

namespace FinalProject_MVC.Areas.JobSeeker.Controllers
{
    [Area("JobSeeker")]
    [Authorize(Roles = RoleConstants.JobSeekerRole)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "داشبورد";
            return View();
        }
    }
}
