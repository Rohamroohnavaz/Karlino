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
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly FinalDbContext _dbContext;

        public ProfileController(UserManager<User> userManager, FinalDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return NotFound();
            }

            var resume = await _dbContext.Resumes
                .FirstOrDefaultAsync(r => r.UserId == user.Id);

            var viewModel = new JobSeekerProfileViewModel
            {
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? "",
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber ?? "",

                JobTitle = resume?.Title ?? "",
                AboutMe = resume?.AboutMe ?? "",
                City = resume?.City ?? "",
                Address = resume?.Address ?? "",
                BirthDate = resume?.BirthDate,
                Gender = resume?.Gender ?? "",
                LinkedInUrl = resume?.LinkedInUrl ?? "",
                GitHubUrl = resume?.GitHubUrl ?? "",
                ProfileImageUrl = resume?.ProfileImageUrl ?? ""
            };

            ViewData["Title"] = "پروفایل من";
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(JobSeekerProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.Identity?.Name;
                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    return NotFound();
                }

                SetProperty(user, "FirstName", viewModel.FirstName);
                SetProperty(user, "LastName", viewModel.LastName);
                user.PhoneNumber = viewModel.PhoneNumber;
                await _userManager.UpdateAsync(user);

                var resume = await _dbContext.Resumes
                    .FirstOrDefaultAsync(r => r.UserId == user.Id);

                if (resume == null)
                {
                    resume = (RequestResume)Activator.CreateInstance(typeof(RequestResume), nonPublic: true);
                    SetProperty(resume, "UserId", user.Id);
                    _dbContext.Resumes.Add(resume);
                }

                SetProperty(resume, "Title", viewModel.JobTitle);
                SetProperty(resume, "AboutMe", viewModel.AboutMe);
                SetProperty(resume, "Address", viewModel.Address);
                SetProperty(resume, "City", viewModel.City);
                SetProperty(resume, "BirthDate", viewModel.BirthDate);
                SetProperty(resume, "Gender", viewModel.Gender);
                SetProperty(resume, "LinkedInUrl", viewModel.LinkedInUrl);
                SetProperty(resume, "GitHubUrl", viewModel.GitHubUrl);

                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "پروفایل با موفقیت بروزرسانی شد";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Title"] = "پروفایل من";
            return View(viewModel);
        }

        private void SetProperty<T>(T entity, string propertyName, object value)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property != null && property.CanWrite == false)
            {
                property.SetValue(entity, value);
            }
        }
    }
}