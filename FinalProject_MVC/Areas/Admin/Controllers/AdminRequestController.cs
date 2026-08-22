using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Infrastructure;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Application.Constants;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class AdminRequestsController : Controller
    {
        private readonly FinalDbContext _dbContext;

        public AdminRequestsController(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _dbContext.Resumes
                .Include(r => r.Advertisement)
                .Include(r => r.User)
                .OrderByDescending(r => r.StartDate)
                .ToListAsync();

            ViewData["Title"] = "مدیریت درخواست‌های رزومه";
            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = await _dbContext.Resumes.FindAsync(id);
            if (request == null)
            {
                TempData["ErrorMessage"] = "درخواست یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            _dbContext.Resumes.Remove(request);
            await _dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "درخواست با موفقیت حذف شد";
            return RedirectToAction(nameof(Index));
        }
    }
}