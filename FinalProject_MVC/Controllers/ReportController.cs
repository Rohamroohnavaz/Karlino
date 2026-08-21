using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;

namespace FinalProject_MVC.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly IUserRepository _userRepository;

        public ReportController(IReportRepository reportRepository, IUserRepository userRepository)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Guid advertisementId, string reason)
        {
            var referer = Request.Headers["Referer"].ToString();

            if (!Url.IsLocalUrl(referer))
                referer = "/";

            if (string.IsNullOrWhiteSpace(reason))
            {
                TempData["Error"] = "لطفاً دلیل گزارش را بنویسید.";
                return Redirect(referer);
            }

            var email = User.Identity?.Name;

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");

            var userId = await _userRepository.GetIdByEmailAsync(email);

            if (userId == null)
                return RedirectToAction("Login", "Account");

            await _reportRepository.AddAsync(advertisementId, userId.Value, reason.Trim());

            TempData["Success"] = "گزارش شما با موفقیت ثبت شد و پس از بررسی توسط مدیریت، اقدام لازم انجام می‌شود.";

            return Redirect(referer);
        }
    }
}