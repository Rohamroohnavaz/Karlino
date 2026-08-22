using FinalProject_MVC.Areas.JobSeeker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Constants;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;

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

                var resume = await _dbContext.Resumes
                    .FirstOrDefaultAsync(r => r.UserId == user.Id);

                if (resume == null)
                {
                    TempData["ErrorMessage"] = "لطفاً ابتدا رزومه خود را در بخش 'رزومه من' تکمیل کنید";
                    return RedirectToAction("Index", "Resume");
                }

                System.Diagnostics.Debug.WriteLine($"=== APPLY METHOD ===");
                System.Diagnostics.Debug.WriteLine($"advertisementId: {advertisementId}");
                System.Diagnostics.Debug.WriteLine($"userId: {user.Id}");
                System.Diagnostics.Debug.WriteLine($"===================");

                var attachId = Guid.NewGuid();
                var attachSql = @"
                     INSERT INTO Attaches (Id, FilePath, FileName, ContentType, FileSize, CompanyId, AdvertisementId, CreatedAt, IsDeleted)
                     VALUES (@Id, @FilePath, @FileName, @ContentType, @FileSize, @CompanyId, @AdvertisementId, @CreatedAt, @IsDeleted)";

                var attachParams = new[]
                {
                   new Microsoft.Data.SqlClient.SqlParameter("@Id", attachId),
                   new Microsoft.Data.SqlClient.SqlParameter("@FilePath", "/resumes/temp.pdf"),
                   new Microsoft.Data.SqlClient.SqlParameter("@FileName", "resume.pdf"),
                   new Microsoft.Data.SqlClient.SqlParameter("@ContentType", "application/pdf"),
                   new Microsoft.Data.SqlClient.SqlParameter("@FileSize", 1024),
                   new Microsoft.Data.SqlClient.SqlParameter("@CompanyId", advertisement.CompanyId),
                   new Microsoft.Data.SqlClient.SqlParameter("@AdvertisementId", advertisementId),
                   new Microsoft.Data.SqlClient.SqlParameter("@CreatedAt", DateTime.Now),
                   new Microsoft.Data.SqlClient.SqlParameter("@IsDeleted", 0)
                };

                await _dbContext.Database.ExecuteSqlRawAsync(attachSql, attachParams);

                var requestSql = @"
                 INSERT INTO RequestResumes (
                 Id, JobSeekerName, JobSeekerLastName, Province, City,
                 StartDate, ExpireDate, UserId, AdvertisementId, AttachmentId,
                 Title, AboutMe, Description, Address, Status, CreatedAt, IsDeleted
                )
                 VALUES (
                 @Id, @JobSeekerName, @JobSeekerLastName, @Province, @City,
                 @StartDate, @ExpireDate, @UserId, @AdvertisementId, @AttachmentId,
                 @Title, @AboutMe, @Description, @Address, @Status, @CreatedAt, @IsDeleted
                )";

                var requestParams = new[]
                {
                   new Microsoft.Data.SqlClient.SqlParameter("@Id", Guid.NewGuid()),
                   new Microsoft.Data.SqlClient.SqlParameter("@JobSeekerName", user.FirstName ?? "نام"),
                   new Microsoft.Data.SqlClient.SqlParameter("@JobSeekerLastName", user.LastName ?? "نام خانوادگی"),
                   new Microsoft.Data.SqlClient.SqlParameter("@Province", "تهران"),
                   new Microsoft.Data.SqlClient.SqlParameter("@City", resume.City ?? "تهران"),
                   new Microsoft.Data.SqlClient.SqlParameter("@StartDate", DateTime.Now),
                   new Microsoft.Data.SqlClient.SqlParameter("@ExpireDate", DateTime.Now.AddMonths(3)),
                   new Microsoft.Data.SqlClient.SqlParameter("@UserId", user.Id),
                   new Microsoft.Data.SqlClient.SqlParameter("@AdvertisementId", advertisementId),
                   new Microsoft.Data.SqlClient.SqlParameter("@AttachmentId", attachId),
                   new Microsoft.Data.SqlClient.SqlParameter("@Title", resume.Title ?? ""),
                   new Microsoft.Data.SqlClient.SqlParameter("@AboutMe", resume.AboutMe ?? ""),
                   new Microsoft.Data.SqlClient.SqlParameter("@Description", resume.AboutMe ?? ""),
                   new Microsoft.Data.SqlClient.SqlParameter("@Address", resume.Address ?? ""),
                   new Microsoft.Data.SqlClient.SqlParameter("@Status", 0),
                   new Microsoft.Data.SqlClient.SqlParameter("@CreatedAt", DateTime.Now),
                   new Microsoft.Data.SqlClient.SqlParameter("@IsDeleted", 0)
                };

                await _dbContext.Database.ExecuteSqlRawAsync(requestSql, requestParams);

                TempData["SuccessMessage"] = $"درخواست شما برای آگهی '{advertisement.Title}' با موفقیت ارسال شد!";
                return RedirectToAction(nameof(Details), new { id = advertisementId });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"!!! ERROR: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"!!! StackTrace: {ex.StackTrace}");

                TempData["ErrorMessage"] = $"خطا در ارسال درخواست: {ex.Message}";
                return RedirectToAction(nameof(Details), new { id = advertisementId });
            }
        }

        //private string GetJobTypeText(int jobType)
        //{
        //    return jobType switch
        //    {
        //        1 => "تمام وقت",
        //        2 => "پاره وقت",
        //        3 => "دورکاری",
        //        4 => "پروژه‌ای",
        //        5 => "کارآموزی",
        //        _ => "نامشخص"
        //    };
        //}

        private async Task<User> GetUserAsync()
        {
            var userEmail = User.Identity?.Name;
            return await _userManager.FindByEmailAsync(userEmail);
        }

        //private void SetProperty<T>(T entity, string propertyName, object value)
        //{
        //    var property = typeof(T).GetProperty(propertyName,
        //        System.Reflection.BindingFlags.Public |
        //        System.Reflection.BindingFlags.NonPublic |
        //        System.Reflection.BindingFlags.Instance);

        //    if (property != null)
        //    {
        //        try
        //        {
        //            if (value != null)
        //            {
        //                var underlyingType = Nullable.GetUnderlyingType(property.PropertyType);
        //                if (underlyingType != null)
        //                {
        //                    value = Convert.ChangeType(value, underlyingType);
        //                }
        //                else if (property.PropertyType != value.GetType())
        //                {
        //                    value = Convert.ChangeType(value, property.PropertyType);
        //                }
        //            }

        //            property.SetValue(entity, value);
        //        }
        //        catch (Exception ex)
        //        {
        //            System.Diagnostics.Debug.WriteLine($"SetProperty Error for {propertyName}: {ex.Message}");
        //        }
        //    }
        //}

        //private void SetStringProperty<T>(T entity, string propertyName, string value)
        //{
        //    value ??= "";

        //    var property = typeof(T).GetProperty(propertyName,
        //        System.Reflection.BindingFlags.Public |
        //        System.Reflection.BindingFlags.NonPublic |
        //        System.Reflection.BindingFlags.Instance);

        //    if (property != null)
        //    {
        //        try
        //        {
        //            property.SetValue(entity, value);
        //        }
        //        catch { }
        //    }
        //}
    }
}