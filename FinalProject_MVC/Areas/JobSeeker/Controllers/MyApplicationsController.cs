using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinalProject_MVC.Areas.JobSeeker.ViewModels;
using MyFinalProject.Infrastructure;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Application.Constants;

namespace FinalProject_MVC.Areas.JobSeeker.Controllers
{
    [Area("JobSeeker")]
    [Authorize(Roles = RoleConstants.JobSeekerRole)]
    public class MyApplicationsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly FinalDbContext _dbContext;

        public MyApplicationsController(UserManager<User> userManager, FinalDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var applications = await _dbContext.Resumes
                .Where(r => r.UserId == user.Id)
                .Include(r => r.Advertisement)
                    .ThenInclude(a => a != null ? a.Company : null)
                .ToListAsync();

            foreach (var app in applications)
            {
                System.Diagnostics.Debug.WriteLine($"Request ID: {app.Id}");
                System.Diagnostics.Debug.WriteLine($"Advertisement: {app.Advertisement?.Title ?? "NULL"}");
                System.Diagnostics.Debug.WriteLine($"Company: {app.Advertisement?.CompanyName ?? "NULL"}");
            }

            var viewModel = applications.Select(r => new MyApplicationsViewModel
            {
                Id = r.Id,
                JobTitle = r.Advertisement != null && !string.IsNullOrEmpty(r.Advertisement.Title)
                    ? r.Advertisement.Title
                    : "عنوان شغل نامشخص",
                CompanyName = r.Advertisement != null && r.Advertisement.Company != null && !string.IsNullOrEmpty(r.Advertisement.Company.CompanyName)
                    ? r.Advertisement.Company.CompanyName
                    : "شرکت نامشخص",
                City = r.Advertisement?.City ?? r.City ?? "",
                AppliedDate = r.StartDate,
                Status = GetStatusText(r.Status),
                StatusBadgeClass = GetStatusBadgeClass(r.Status),
                AdvertisementId = r.AdvertisementId
            }).ToList();

            ViewData["Title"] = "درخواست‌های من";
            return View(viewModel);
        }

        private string GetStatusText(MyFinalProject.Domain.Entities.Enums.RequestStatus status)
        {
            return status switch
            {
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Pending => "در انتظار بررسی",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.CurrentlyViewing => "در حال بررسی",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Interview => "دعوت به مصاحبه",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Success => "پذیرفته شده",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Fail => "رد شده",
                _ => "نامشخص"
            };
        }

        private string GetStatusBadgeClass(MyFinalProject.Domain.Entities.Enums.RequestStatus status)
        {
            return status switch
            {
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Pending => "bg-secondary",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.CurrentlyViewing => "bg-warning text-dark",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Interview => "bg-warning text-dark",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Success => "bg-success",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Fail => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
}