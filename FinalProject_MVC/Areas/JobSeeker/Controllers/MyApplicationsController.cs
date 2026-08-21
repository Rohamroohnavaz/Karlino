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
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            var viewModel = applications.Select(r => new MyApplicationsViewModel
            {
                Id = r.Id,
                JobTitle = r.Advertisement?.Title ?? "عنوان شغل نامشخص",
                CompanyName = r.Advertisement?.CompanyName ?? "شرکت نامشخص",
                City = r.Advertisement?.City ?? "",
                AppliedDate = r.CreatedAt,
                Status = GetStatusText(((int)r.Status)),
                StatusBadgeClass = GetStatusBadgeClass(((int)r.Status))
            }).ToList();

            ViewData["Title"] = "درخواست‌های من";
            return View(viewModel);
        }

        private string GetStatusText(int status)
        {
            return status switch
            {
                0 => "در انتظار بررسی",
                1 => "در حال بررسی",
                2 => "پذیرفته شده",
                3 => "رد شده",
                _ => "نامشخص"
            };
        }

        private string GetStatusBadgeClass(int status)
        {
            return status switch
            {
                0 => "bg-secondary",
                1 => "bg-warning text-dark",
                2 => "bg-success",
                3 => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
}