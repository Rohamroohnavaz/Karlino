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
    public class JobsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly FinalDbContext _dbContext;

        public JobsController(UserManager<User> userManager, FinalDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string jobTitle, string city, string jobType)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var query = _dbContext.Advertisements
                .Where(a => a.IsActive && !a.IsDeleted && a.ExpireDate > DateTime.Now)
                .Include(a => a.Company)
                .OrderByDescending(a => a.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(jobTitle))
            {
                query = query.Where(a => a.Title.Contains(jobTitle));
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(a => a.City == city);
            }

            if (!string.IsNullOrEmpty(jobType))
            {
                query = query.Where(a => a.Title == jobType);
            }

            var advertisements = await query.ToListAsync();

            var userApplications = await _dbContext.Resumes
                .Where(r => r.UserId == user.Id)
                .Select(r => r.AdvertisementId)
                .ToListAsync();

            var viewModel = new JobSearchViewModel
            {
                JobTitle = jobTitle,
                City = city,
                JobType = jobType,
                Jobs = advertisements.Select(a => new JobsViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    CompanyName = a.CompanyName ?? "نامشخص",
                    City = a.City ?? "",
                    Salary = a.Salary.ToString() ?? "توافقی",
                    PostedDate = a.CreatedAt,
                    Description = a.Description ?? "",
                    HasApplied = userApplications.Contains(a.Id)
                }).ToList()
            };

            ViewData["Title"] = "جستجوی شغل";
            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var advertisement = await _dbContext.Advertisements
                .Include(a => a.Company)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsActive && !a.IsDeleted);

            if (advertisement == null) return NotFound();

            var hasApplied = await _dbContext.Resumes
                .AnyAsync(r => r.UserId == user.Id && r.AdvertisementId == id);

            var viewModel = new JobsViewModel
            {
                Id = advertisement.Id,
                Title = advertisement.Title,
                CompanyName = advertisement.CompanyName ?? "نامشخص",
                City = advertisement.City ?? "",
                Salary = advertisement.Salary.ToString() ?? "توافقی",
                PostedDate = advertisement.CreatedAt,
                Description = advertisement.Description ?? "",
                HasApplied = hasApplied
            };

            ViewData["Title"] = advertisement.Title;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(Guid advertisementId)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var advertisement = await _dbContext.Advertisements
                .FirstOrDefaultAsync(a => a.Id == advertisementId && a.IsActive && !a.IsDeleted);

            if (advertisement == null)
            {
                TempData["ErrorMessage"] = "آگهی مورد نظر یافت نشد یا منقضی شده است";
                return RedirectToAction(nameof(Index));
            }

            var existingApplication = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.UserId == user.Id && r.AdvertisementId == advertisementId);

            if (existingApplication != null)
            {
                TempData["ErrorMessage"] = "شما قبلاً برای این آگهی درخواست ارسال کرده‌اید";
                return RedirectToAction(nameof(Details), new { id = advertisementId });
            }

            var resume = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.UserId == user.Id);

            if (resume == null)
            {
                TempData["ErrorMessage"] = "لطفاً ابتدا رزومه خود را تکمیل کنید";
                return RedirectToAction("Index", "Resume");
            }

            var requestResume = (RequestResume)Activator.CreateInstance(typeof(RequestResume), nonPublic: true);

            var jobSeekerName = $"{user.FirstName} {user.LastName}";
            var startDate = DateTime.Now;
            var expireDate = DateTime.Now.AddDays(30);

            typeof(RequestResume).GetProperty("JobSeekerName")?.SetValue(requestResume, jobSeekerName);
            typeof(RequestResume).GetProperty("JobSeekerLastName")?.SetValue(requestResume, user.LastName);
            typeof(RequestResume).GetProperty("Province")?.SetValue(requestResume, resume.Province);
            typeof(RequestResume).GetProperty("City")?.SetValue(requestResume, resume.City);
            typeof(RequestResume).GetProperty("StartDate")?.SetValue(requestResume, startDate);
            typeof(RequestResume).GetProperty("ExpireDate")?.SetValue(requestResume, expireDate);
            typeof(RequestResume).GetProperty("UserId")?.SetValue(requestResume, user.Id);
            typeof(RequestResume).GetProperty("AdvertisementId")?.SetValue(requestResume, advertisementId);
            typeof(RequestResume).GetProperty("AttachmentId")?.SetValue(requestResume, Guid.NewGuid());
            typeof(RequestResume).GetProperty("Status")?.SetValue(requestResume, 0);
            typeof(RequestResume).GetProperty("CreatedAt")?.SetValue(requestResume, DateTime.Now);

            _dbContext.Resumes.Add(requestResume);
            await _dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "درخواست شما با موفقیت ارسال شد";
            return RedirectToAction(nameof(Details), new { id = advertisementId });
        }

        private string GetJobTypeText(int jobType)
        {
            return jobType switch
            {
                1 => "تمام وقت",
                2 => "پاره وقت",
                3 => "دورکاری",
                4 => "پروژه‌ای",
                5 => "کارآموزی",
                _ => "نامشخص"
            };
        }
    }
}