using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    public class SettingsController : AdminBaseController
    {
        private readonly ISettingRepository _settingRepository;

        public SettingsController(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "تنظیمات سایت";

            await _settingRepository.SeedDefaultsAsync();

            return View(await _settingRepository.GetAllAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save()
        {
            foreach (var key in Request.Form.Keys.Where(k => k.StartsWith("setting_")))
            {
                var settingKey = key.Substring("setting_".Length);
                var value = Request.Form[key].ToString();

                await _settingRepository.SetValueAsync(settingKey, value);
            }

            TempData["Success"] = "تنظیمات با موفقیت ذخیره شد.";

            return RedirectToAction(nameof(Index));
        }
    }
}