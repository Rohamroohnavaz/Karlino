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
            try
            {
                var user = await GetUserAsync();
                if (user == null)
                {
                    TempData["ErrorMessage"] = "کاربر یافت نشد";
                    return RedirectToAction(nameof(Index));
                }

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
                    TempData["WarningMessage"] = "شما قبلاً برای این آگهی درخواست ارسال کرده‌اید";
                    return RedirectToAction(nameof(Details), new { id = advertisementId });
                }

                var resume = await _dbContext.Resumes
                    .FirstOrDefaultAsync(r => r.UserId == user.Id);

                if (resume == null)
                {
                    TempData["ErrorMessage"] = "لطفاً ابتدا رزومه خود را در بخش 'رزومه من' تکمیل کنید";
                    return RedirectToAction("Index", "Resume");
                }

                var newRequest = new RequestResume(
                    jobSeekerName: user.FirstName ?? "نام",
                    jobSeekerLastName: user.LastName ?? "نام خانوادگی",
                    province: "تهران",
                    city: resume.City ?? "تهران",
                    startDate: DateTime.Now,
                    expireDate: DateTime.Now.AddMonths(3),
                    userId: user.Id,
                    advertisementId: advertisementId,  
                    attachmentId: Guid.NewGuid()
                );

                SetProperty(newRequest, "Title", resume.Title);
                SetProperty(newRequest, "AboutMe", resume.AboutMe);
                SetProperty(newRequest, "Description", resume.AboutMe);
                SetProperty(newRequest, "Address", resume.Address);
                SetProperty(newRequest, "LinkedInUrl", resume.LinkedInUrl);
                SetProperty(newRequest, "GitHubUrl", resume.GitHubUrl);
                SetProperty(newRequest, "EducationDegree", resume.EducationDegree);
                SetProperty(newRequest, "EducationField", resume.EducationField);
                SetProperty(newRequest, "University", resume.University);
                SetProperty(newRequest, "EducationStartYear", resume.EducationStartYear);
                SetProperty(newRequest, "EducationEndYear", resume.EducationEndYear);
                SetProperty(newRequest, "WorkTitle", resume.WorkTitle);
                SetProperty(newRequest, "CompanyName", resume.CompanyName);
                SetProperty(newRequest, "WorkDescription", resume.WorkDescription);
                SetProperty(newRequest, "WorkStartYear", resume.WorkStartYear);
                SetProperty(newRequest, "WorkEndYear", resume.WorkEndYear);
                SetProperty(newRequest, "Skills", resume.Skills);
                SetProperty(newRequest, "Languages", resume.Languages);
                SetProperty(newRequest, "ResumeFilePath", resume.ResumeFilePath);

                _dbContext.Resumes.Add(newRequest);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = $"درخواست شما برای آگهی '{advertisement.Title}' با موفقیت ارسال شد!";
                return RedirectToAction(nameof(Details), new { id = advertisementId });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در ارسال درخواست: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id = advertisementId });
            }
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

        private async Task<User> GetUserAsync()
        {
            var userEmail = User.Identity?.Name;
            return await _userManager.FindByEmailAsync(userEmail);
        }

        private void SetProperty<T>(T entity, string propertyName, object value)
        {
            var property = typeof(T).GetProperty(propertyName,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            if (property != null)
            {
                try
                {
                    if (value != null && property.PropertyType != value.GetType())
                    {
                        try
                        {
                            value = Convert.ChangeType(value, property.PropertyType);
                        }
                        catch { }
                    }
                    property.SetValue(entity, value);
                }
                catch { }
            }
        }
    }
}