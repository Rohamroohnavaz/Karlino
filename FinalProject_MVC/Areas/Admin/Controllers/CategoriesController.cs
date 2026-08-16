using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {
        private readonly ICategoryRepository _repository;

        public CategoriesController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "مدیریت دسته‌بندی‌ها";
            return View(await _repository.GetAllAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string title, string description)
        {
            if (!string.IsNullOrWhiteSpace(title))
                await _repository.AddAsync(title.Trim() ,(description ?? string.Empty).Trim());

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
                await _repository.UpdateAsync(id, title.Trim());

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch
            {
                TempData["Error"] = "این دسته‌بندی به آگهی‌ها متصل است و قابل حذف نیست.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}