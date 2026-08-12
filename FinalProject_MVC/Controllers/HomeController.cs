using FinalProject_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalProject_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
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

        [AllowAnonymous]
        public IActionResult DebugClaims()
        {
            var isAuthenticated = User.Identity?.IsAuthenticated ?? false;

            var claims = string.Join(
                "<br>",
                User.Claims.Select(c => $"{c.Type} = {c.Value}")
            );

            var html = $@"
            <h2>Debug Claims</h2>
            <p><b>IsAuthenticated:</b> {isAuthenticated}</p>
            <p><b>AuthenticationType:</b> {User.Identity?.AuthenticationType}</p>
            <p><b>Claims:</b></p>
            <div>{claims}</div>
        ";
            return Content(html, "text/html");
        }
    }
}

