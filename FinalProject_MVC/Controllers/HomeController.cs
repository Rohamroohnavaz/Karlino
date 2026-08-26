using FinalProject_MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Constants;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FinalProject_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger
            , IAdvertisementRepository advertisementRepository
            , IUserRepository userRepository
            , UserManager<User> userManager
            )
        {
            _logger = logger;
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> Index()
        //{
        //    ViewBag.ActiveJobs = await _advertisementRepository.GetCountByStatus(isActive: true);
        //    ViewBag.Employers = await _userRepository.GetCountByRole(RoleConstants.EmployerRole);
        //    ViewBag.JobSeekers = await _userRepository.GetCountByRole(RoleConstants.JobSeekerRole);

        //    var (latest, _) = await _advertisementRepository.GetPagedForAdminAsync(null, true, 1, 6);
        //    ViewBag.LatestJobs = latest;

        //    return View();
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);

                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        if (roles.Contains(RoleConstants.JobSeekerRole))
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "JobSeeker" });
                        }
                        else if (roles.Contains("Admin"))
                        {
                            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                        }
                        else if (roles.Contains(RoleConstants.EmployerRole))
                        {
                            // return RedirectToAction("Index", "Dashboard", new { area = "Employer" });
                        }
                    }
                }
                catch (Exception)
                {
                    await HttpContext.SignOutAsync();
                }
            }

            ViewBag.ActiveJobs = await _advertisementRepository.GetCountByStatus(isActive: true);
            ViewBag.Employers = await _userRepository.GetCountByRole(RoleConstants.EmployerRole);
            ViewBag.JobSeekers = await _userRepository.GetCountByRole(RoleConstants.JobSeekerRole);


            var (latest, _) = await _advertisementRepository.GetPagedForAdminAsync(null, true, 1, 6);
            ViewBag.LatestJobs = latest;

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[AllowAnonymous]
        //public IActionResult DebugClaims()
        //{
        //    var isAuthenticated = User.Identity?.IsAuthenticated ?? false;

        //    var claims = string.Join(
        //        "<br>",
        //        User.Claims.Select(c => $"{c.Type} = {c.Value}")
        //    );

        //    var html = $@"
        //    <h2>Debug Claims</h2>
        //    <p><b>IsAuthenticated:</b> {isAuthenticated}</p>
        //    <p><b>AuthenticationType:</b> {User.Identity?.AuthenticationType}</p>
        //    <p><b>Claims:</b></p>
        //    <div>{claims}</div>
        //";
        //    return Content(html, "text/html");
        //}
    }
}

