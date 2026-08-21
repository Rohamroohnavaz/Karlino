using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinalProject_MVC.Areas.JobSeeker.ViewModels;
using MyFinalProject.Infrastructure;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Application.Constants;
using System.Reflection;

namespace FinalProject_MVC.Areas.JobSeeker.Controllers
{
    [Area("JobSeeker")]
    [Authorize(Roles = RoleConstants.JobSeekerRole)]
    public class ResumeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly FinalDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ResumeController(
            UserManager<User> userManager,
            FinalDbContext dbContext,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: JobSeeker/Resume
        public async Task<IActionResult> Index()
        {
            var user = await GetUserAsync();
            if (user == null) return NotFound();

            var resume = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.UserId == user.Id);

            if (resume == null)
            {
                var viewModel = new ResumeViewModel
                {
                    FullName = $"{user.FirstName} {user.LastName}",
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    City = user.City ?? ""
                };

                ViewData["Title"] = "رزومه من";
                return View(viewModel);
            }

            var existingViewModel = new ResumeViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                JobTitle = resume.Title ?? "",
                AboutMe = resume.AboutMe ?? "",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                City = resume.City ?? "",
                Address = resume.Address ?? "",
                LinkedInUrl = resume.LinkedInUrl ?? "",
                GitHubUrl = resume.GitHubUrl ?? "",
                EducationDegree = resume.EducationDegree ?? "",
                EducationField = resume.EducationField ?? "",
                University = resume.University ?? "",
                EducationStartYear = resume.EducationStartYear,
                EducationEndYear = resume.EducationEndYear,
                WorkTitle = resume.WorkTitle ?? "",
                CompanyName = resume.CompanyName ?? "",
                WorkDescription = resume.WorkDescription ?? "",
                WorkStartYear = resume.WorkStartYear,
                WorkEndYear = resume.WorkEndYear,
                Skills = resume.Skills ?? "",
                Languages = resume.Languages ?? "",
                ResumeFilePath = resume.ResumeFilePath ?? ""
            };

            ViewData["Title"] = "رزومه من";
            return View(existingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ResumeViewModel viewModel, IFormFile resumeFile)
        {
            try
            {
                var user = await GetUserAsync();
                if (user == null) return NotFound();

                var resume = await _dbContext.Resumes
                    .FirstOrDefaultAsync(r => r.UserId == user.Id);

                if (resume == null)
                {
                    resume = (RequestResume)Activator.CreateInstance(typeof(RequestResume), nonPublic: true);
                    SetProperty(resume, "UserId", user.Id);
                    _dbContext.Resumes.Add(resume);
                }

                SetStringProperty(resume, "Title", viewModel.JobTitle);
                SetStringProperty(resume, "AboutMe", viewModel.AboutMe);
                SetStringProperty(resume, "City", viewModel.City);
                SetStringProperty(resume, "Address", viewModel.Address);
                SetStringProperty(resume, "LinkedInUrl", viewModel.LinkedInUrl);
                SetStringProperty(resume, "GitHubUrl", viewModel.GitHubUrl);
                SetStringProperty(resume, "EducationDegree", viewModel.EducationDegree);
                SetStringProperty(resume, "EducationField", viewModel.EducationField);
                SetStringProperty(resume, "University", viewModel.University);
                SetStringProperty(resume, "WorkTitle", viewModel.WorkTitle);
                SetStringProperty(resume, "CompanyName", viewModel.CompanyName);
                SetStringProperty(resume, "WorkDescription", viewModel.WorkDescription);
                SetStringProperty(resume, "Skills", viewModel.Skills);
                SetStringProperty(resume, "Languages", viewModel.Languages);

                SetStringProperty(resume, "Description", viewModel.AboutMe);

                SetStringProperty(resume, "JobSeekerName", user.FirstName ?? "نام");
                SetStringProperty(resume, "JobSeekerLastName", user.LastName ?? "نام خانوادگی");
                SetStringProperty(resume, "Province", "تهران");
                SetStringProperty(resume, "Gender", "");
                SetStringProperty(resume, "ProfileImageUrl", "");
                SetStringProperty(resume, "ResumeFilePath", "");

                SetProperty(resume, "EducationStartYear", viewModel.EducationStartYear);
                SetProperty(resume, "EducationEndYear", viewModel.EducationEndYear);
                SetProperty(resume, "WorkStartYear", viewModel.WorkStartYear);
                SetProperty(resume, "WorkEndYear", viewModel.WorkEndYear);

                if (resumeFile != null && resumeFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "resumes");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(resumeFile.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resumeFile.CopyToAsync(fileStream);
                    }

                    if (System.IO.File.Exists(filePath))
                    {
                        SetStringProperty(resume, "ResumeFilePath", $"/resumes/{uniqueFileName}");
                    }
                }

                SetProperty(resume, "StartDate", DateTime.Now);
                SetProperty(resume, "ExpireDate", DateTime.Now.AddYears(1));
                SetProperty(resume, "CreatedAt", DateTime.Now);
                SetProperty(resume, "ModifiedAt", DateTime.Now);

                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "رزومه با موفقیت ذخیره شد";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در ذخیره رزومه: {ex.Message}";
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Download()
        {
            var user = await GetUserAsync();
            if (user == null) return NotFound();

            var resume = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.UserId == user.Id);

            if (resume == null || string.IsNullOrEmpty(resume.ResumeFilePath))
            {
                return NotFound();
            }

            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, resume.ResumeFilePath.TrimStart('/'));

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/pdf", "Resume.pdf");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
        {
            var user = await GetUserAsync();
            if (user == null) return NotFound();

            var resume = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.UserId == user.Id);

            if (resume != null)
            {
                if (!string.IsNullOrEmpty(resume.ResumeFilePath))
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, resume.ResumeFilePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _dbContext.Resumes.Remove(resume);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "رزومه با موفقیت حذف شد";
            }

            return RedirectToAction(nameof(Index));
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

        private void SetStringProperty<T>(T entity, string propertyName, string value)
        {
            value ??= ""; 

            var property = typeof(T).GetProperty(propertyName,
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance);

            if (property != null)
            {
                try
                {
                    property.SetValue(entity, value);
                }
                catch { }
            }
        }
    }
}