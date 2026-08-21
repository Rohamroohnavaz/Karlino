using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Infrastructure;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Application.Constants;
using MyFinalProject.Infrastructure.DTO;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly FinalDbContext _dbContext;

        public UsersController(UserManager<User> userManager, FinalDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await _dbContext.Users
                .Where(u => u.Role.ToString() == RoleConstants.JobSeekerRole.ToString() ||
                           u.Role.ToString() == RoleConstants.EmployerRole.ToString() ||
                           u.Role.ToString() == RoleConstants.AdminRole.ToString())
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            var viewModel = users.Select(u => new AdminUserTableDto
            {
                Id = u.Id,
                Email = u.Email ?? "",
                FullName = $"{u.FirstName} {u.LastName}",
                RoleName = u.Role.ToString() ?? "",
                PhoneNumber = u.PhoneNumber ?? "",
                City = u.City ?? "",
                IsApproved = u.IsApproved,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            }).ToList();

            ViewData["Title"] = "مدیریت کاربران";
            return View(viewModel);
        }

        // POST: Admin/Users/Approve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                TempData["ErrorMessage"] = "کاربر یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            // استفاده از Reflection برای تغییر IsApproved
            var isApprovedProperty = typeof(User).GetProperty("IsApproved");
            if (isApprovedProperty != null)
            {
                isApprovedProperty.SetValue(user, true);
            }

            await _dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = $"کاربر {user.FirstName} {user.LastName} با موفقیت تایید شد";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Users/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                TempData["ErrorMessage"] = "کاربر یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            var isActiveProperty = typeof(User).GetProperty("IsActive");
            if (isActiveProperty != null)
            {
                isActiveProperty.SetValue(user, false);
            }

            await _dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = $"کاربر {user.FirstName} {user.LastName} رد شد";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Users/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                TempData["ErrorMessage"] = "کاربر یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "کاربر با موفقیت حذف شد";
            return RedirectToAction(nameof(Index));
        }
    }
}