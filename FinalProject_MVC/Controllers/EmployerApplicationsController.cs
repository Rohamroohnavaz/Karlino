using FinalProject_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Constants;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;

namespace FinalProject_MVC.Areas.Employer.Controllers
{
    [Authorize(Roles = RoleConstants.EmployerRole)]
    public class EmployerApplicationsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly FinalDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployerApplicationsController(UserManager<User> userManager
            , FinalDbContext dbContext
            , IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //public async Task<IActionResult> Index(Guid? advertisementId)
        //{
        //    var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
        //    if (user == null) return NotFound();

        //    var requests = await _dbContext.Resumes
        //        .Include(r => r.Advertisement)
        //            .ThenInclude(a => a.Company)
        //        .Where(r => r.Advertisement != null &&
        //                    r.Advertisement.Company != null &&
        //                    r.Advertisement.Company.UserId == user.Id)
        //        .OrderByDescending(r => r.StartDate)
        //        .ToListAsync();

        //    if (advertisementId.HasValue)
        //    {
        //        requests = requests.Where(r => r.AdvertisementId == advertisementId.Value).ToList();
        //    }

        //    var viewModel = requests.Select(r => new EmployerApplicationsViewModel
        //    {
        //        RequestId = r.Id,
        //        JobSeekerName = r.JobSeekerName ?? "نامشخص",
        //        JobSeekerLastName = r.JobSeekerLastName ?? "",
        //        City = r.City ?? "",
        //        Title = r.Title ?? "",
        //        Skills = r.Skills ?? "",
        //        ResumeFilePath = r.ResumeFilePath,
        //        AppliedDate = r.StartDate,
        //        Status = GetStatusText(r.Status),
        //        StatusBadgeClass = GetStatusBadgeClass(r.Status),
        //        StatusValue = (int)r.Status,
        //        AdvertisementId = r.AdvertisementId ?? Guid.Empty,
        //        AdvertisementTitle = r.Advertisement?.Title ?? "نامشخص"
        //    }).ToList();

        //    ViewBag.TotalRequestsInDB = await _dbContext.Resumes.CountAsync();
        //    ViewBag.AdsCountForThisUser = await _dbContext.Advertisements.CountAsync(a => a.Company != null && a.Company.UserId == user.Id);
        //    ViewBag.MatchedRequestsCount = viewModel.Count;

        //    ViewData["Title"] = "درخواست‌های دریافتی";
        //    return View(viewModel);
        //}

        public async Task<IActionResult> Index(Guid? advertisementId)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var requests = await _dbContext.Resumes
                .Include(r => r.Advertisement)
                .ThenInclude(a => a.Company)
                .Where(r => r.Advertisement != null &&
                            r.Advertisement.Company != null &&
                            r.Advertisement.Company.UserId == user.Id)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();

            TempData["DebugInfo"] = $"تعداد درخواست‌ها: {requests.Count}<br/>" +
                                   $"UserId کارفرما: {user.Id}";

            foreach (var req in requests)
            {
                TempData["DebugInfo"] += $"<br/>- {req.JobSeekerName} | AdId: {req.AdvertisementId} | Company UserId: {req.Advertisement?.Company?.UserId}";
            }

            var viewModel = requests.Select(r => new EmployerApplicationsViewModel
            {
                RequestId = r.Id,
                JobSeekerName = r.JobSeekerName ?? "نامشخص",
                JobSeekerLastName = r.JobSeekerLastName ?? "",
                City = r.City ?? "",
                Title = r.Title ?? "",
                Skills = r.Skills ?? "",
                ResumeFilePath = r.ResumeFilePath,
                AppliedDate = r.StartDate,
                Status = GetStatusText(r.Status),
                StatusBadgeClass = GetStatusBadgeClass(r.Status),
                StatusValue = (int)r.Status,
                AdvertisementId = r.AdvertisementId ?? Guid.Empty,
                AdvertisementTitle = r.Advertisement?.Title ?? "نامشخص"
            }).ToList();

            ViewData["Title"] = "درخواست‌های دریافتی";
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(Guid requestId, int newStatus)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var request = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
            {
                TempData["ErrorMessage"] = "درخواست یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            var advertisement = await _dbContext.Advertisements
                .FirstOrDefaultAsync(a => a.Id == request.AdvertisementId && a.Company.UserId == user.Id);

            if (advertisement == null)
            {
                TempData["ErrorMessage"] = "شما به این درخواست دسترسی ندارید";
                return RedirectToAction(nameof(Index));
            }

            // تغییر وضعیت
            var statusEnum = (RequestStatus)newStatus;
            request.SetStatus(statusEnum);

            await _dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = $"وضعیت درخواست {request.JobSeekerName} {request.JobSeekerLastName} به '{GetStatusText(statusEnum)}' تغییر کرد";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ViewResume(Guid requestId)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var request = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
            {
                TempData["ErrorMessage"] = "درخواست یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            var advertisement = await _dbContext.Advertisements
                .FirstOrDefaultAsync(a => a.Id == request.AdvertisementId && a.Company.UserId == user.Id);

            if (advertisement == null)
            {
                TempData["ErrorMessage"] = "شما به این درخواست دسترسی ندارید";
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrEmpty(request.ResumeFilePath))
            {
                TempData["WarningMessage"] = "فایل رزومه‌ای آپلود نشده است";
                return RedirectToAction(nameof(Index));
            }

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, request.ResumeFilePath.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                TempData["ErrorMessage"] = "فایل رزومه پیدا نشد";
                return RedirectToAction(nameof(Index));
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", $"Resume_{request.JobSeekerName}_{request.JobSeekerLastName}.pdf");
        }

        private string GetStatusText(RequestStatus status)
        {
            return status switch
            {
                RequestStatus.Pending => "در انتظار بررسی",
                RequestStatus.CurrentlyViewing => "در حال بررسی",
                RequestStatus.Interview => "دعوت برای مصاحبه",
                RequestStatus.Success => "پذیرفته شده",
                RequestStatus.Fail => "رد شده",
                _ => "نامشخص"
            };
        }

        private string GetStatusBadgeClass(RequestStatus status)
        {
            return status switch
            {
                RequestStatus.Pending => "bg-secondary",
                RequestStatus.CurrentlyViewing => "bg-warning text-dark",
                RequestStatus.Interview => "bg-warning text-dark",
                RequestStatus.Success => "bg-success",
                RequestStatus.Fail => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
}