//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using FinalProject_MVC.Areas.JobSeeker.ViewModels;
//using FinalProject_MVC.Models;
//using MyFinalProject.Infrastructure;
//using Microsoft.EntityFrameworkCore;
//using FinalProject_MVC.Areas.JobSeeker.Controllers.Base; // مدل User و Resume خودت

//namespace FinalProject_MVC.Areas.JobSeeker.Controllers
//{
//    [Area("JobSeeker")]
//    [Authorize(Roles = "JobSeeker")]
//    public class ProfileController : JobSeekerBaseController
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly FinalDbContext _dbContext;

//        public ProfileController(UserManager<IdentityUser> userManager, FinalDbContext dbContext)
//        {
//            _userManager = userManager;
//            _dbContext = dbContext;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            var resume = await _dbContext.Resumes
//                .FirstOrDefaultAsync(r => r.UserId.ToString() == user.Id);

//            var viewModel = new JobSeekerProfileViewModel
//            {
//                FirstName = user.nam ?? "",
//                LastName = user.LastName ?? "",
//                Email = user.Email ?? "",
//                PhoneNumber = user.PhoneNumber ?? "",
//                // فیلدهای دیگر از resume
//                JobTitle = resume?.JobTitle ?? "",
//                AboutMe = resume?.AboutMe ?? "",
//                City = resume?.City ?? "",
//                Address = resume?.Address ?? "",
//                BirthDate = resume?.BirthDate,
//                Gender = resume?.Gender,
//                LinkedInUrl = resume?.LinkedInUrl,
//                GitHubUrl = resume?.GitHubUrl,
//                ProfileImageUrl = resume?.ProfileImageUrl
//            };

//            ViewData["Title"] = "پروفایل من";
//            return View(viewModel);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Index(JobSeekerProfileViewModel viewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.GetUserAsync(User);
//                if (user == null)
//                {
//                    return NotFound();
//                }

//                user.FirstName = viewModel.FirstName;
//                user.LastName = viewModel.LastName;
//                user.PhoneNumber = viewModel.PhoneNumber;
//                await _userManager.UpdateAsync(user);

//                // بروزرسانی یا ایجاد رزومه
//                var resume = await _context.Resumes
//                    .FirstOrDefaultAsync(r => r.UserId == user.Id);

//                if (resume == null)
//                {
//                    resume = new Resume { UserId = user.Id };
//                    _context.Resumes.Add(resume);
//                }

//                resume.JobTitle = viewModel.JobTitle;
//                resume.AboutMe = viewModel.AboutMe;
//                resume.City = viewModel.City;
//                resume.Address = viewModel.Address;
//                resume.BirthDate = viewModel.BirthDate;
//                resume.Gender = viewModel.Gender;
//                resume.LinkedInUrl = viewModel.LinkedInUrl;
//                resume.GitHubUrl = viewModel.GitHubUrl;

//                await _context.SaveChangesAsync();

//                TempData["SuccessMessage"] = "پروفایل با موفقیت بروزرسانی شد";
//                return RedirectToAction(nameof(Index));
//            }

//            ViewData["Title"] = "پروفایل من";
//            return View(viewModel);
//        }
//    }
//}