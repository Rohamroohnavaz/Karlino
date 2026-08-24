using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.Requests;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAdminService _adminService;

        public UsersController(UserManager<User> userManager
            , IAdminService adminService)
        {
            _userManager = userManager;
            _adminService = adminService;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var viewModel = await _adminService.GetAllUsersAsync();
            ViewData["Title"] = "مدیریت کاربران";
            return View(viewModel);
        }

        #region Divide Methods

        // (Employer)

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ApproveEmployer(Guid id)
        //{
        //    var user = await _userManager.FindByIdAsync(id.ToString());
        //    if (user == null) return NotFound();

        //    var emailRequest = new SendEmailRequest
        //    {
        //        To = user.Email,
        //        Subject = "تأیید حساب کاربری کارفرما",
        //        Body = "حساب کاربری شما با موفقیت توسط مدیریت تأیید شد. اکنون می‌توانید وارد پنل خود شوید.",
        //        isHtml = false
        //    };

        //    await _adminService.ApproveEmployerAsync(emailRequest, id, CancellationToken.None);

        //    TempData["SuccessMessage"] = "حساب کارفرما با موفقیت تأیید شد.";
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RejectEmployer(Guid id)
        //{
        //    var user = await _userManager.FindByIdAsync(id.ToString());
        //    if (user == null) return NotFound();

        //    var emailRequest = new SendEmailRequest
        //    {
        //        To = user.Email,
        //        Subject = "رد درخواست ثبت‌نام کارفرما",
        //        Body = "متأسفانه درخواست ثبت‌نام شما مورد تأیید مدیریت قرار نگرفت.",
        //        isHtml = false
        //    };

        //    await _adminService.RejectEmployersAsync(emailRequest, id, CancellationToken.None);

        //    TempData["SuccessMessage"] = "حساب کارفرما رد شد.";
        //    return RedirectToAction(nameof(Index));
        //}

        //// (JobSeeker)

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ApproveJobSeeker(Guid id)
        //{
        //    var user = await _userManager.FindByIdAsync(id.ToString());
        //    if (user == null) return NotFound();

        //    var emailRequest = new SendEmailRequest
        //    {
        //        To = user.Email,
        //        Subject = "تأیید حساب کاربری کارجو",
        //        Body = "حساب کاربری شما با موفقیت تأیید شد. اکنون می‌توانید رزومه ارسال کنید.",
        //        isHtml = false
        //    };

        //    await _adminService.ApproveJobSeekerAsync(emailRequest, id, CancellationToken.None);

        //    TempData["SuccessMessage"] = "حساب کارجو با موفقیت تأیید شد.";
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RejectJobSeeker(Guid id)
        //{
        //    var user = await _userManager.FindByIdAsync(id.ToString());
        //    if (user == null) return NotFound();

        //    var emailRequest = new SendEmailRequest
        //    {
        //        To = user.Email,
        //        Subject = "رد درخواست ثبت‌نام کارجو",
        //        Body = "متأسفانه درخواست ثبت‌نام شما مورد تأیید مدیریت قرار نگرفت.",
        //        isHtml = false
        //    };

        //    await _adminService.RejectJobSeekerAsync(emailRequest, id, CancellationToken.None); // فرض بر این است که متد RejectJobSeekerAsync در سرویس دارید

        //    TempData["SuccessMessage"] = "حساب کارجو رد شد.";
        //    return RedirectToAction(nameof(Index));
        //}

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var emailRequest = new SendEmailRequest
            {
                To = user.Email,
                Subject = user.Role.ToString() == "Employer" ? "تأیید حساب کاربری کارفرما" : "تأیید حساب کاربری کارجو",
                Body = user.Role.ToString() == "Employer"
                    ? "حساب کاربری شما با موفقیت توسط مدیریت تأیید شد. اکنون می‌توانید وارد پنل کارفرمایی خود شوید."
                    : "حساب کاربری شما با موفقیت تأیید شد. اکنون می‌توانید رزومه ارسال کنید و فرصت‌های شغلی را ببینید.",
                isHtml = false
            };

            try
            {
                if (user.Role.ToString() == "Employer")
                {
                    await _adminService.ApproveEmployerAsync(emailRequest, id, CancellationToken.None);
                }
                else if (user.Role.ToString() == "JobSeeker")
                {
                    await _adminService.ApproveJobSeekerAsync(emailRequest, id, CancellationToken.None);
                }

                TempData["SuccessMessage"] = "کاربر با موفقیت تأیید شد و ایمیل اطلاع‌رسانی ارسال گردید.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در تأیید کاربر: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var emailRequest = new SendEmailRequest
            {
                To = user.Email,
                Subject = user.Role.ToString() == "Employer" ? "رد درخواست ثبت‌نام کارفرما" : "رد درخواست ثبت‌نام کارجو",
                Body = user.Role.ToString() == "Employer"
                    ? "متأسفانه درخواست ثبت‌نام شما به عنوان کارفرما مورد تأیید مدیریت قرار نگرفت."
                    : "متأسفانه درخواست ثبت‌نام شما مورد تأیید مدیریت قرار نگرفت.",
                isHtml = false
            };

            try
            {
                if (user.Role.ToString() == "Employer")
                {
                    await _adminService.RejectEmployersAsync(emailRequest, id, CancellationToken.None);
                }
                else if (user.Role.ToString() == "JobSeeker")
                {
                    await _adminService.RejectJobSeekerAsync(emailRequest, id, CancellationToken.None);
                }

                TempData["SuccessMessage"] = "کاربر رد شد و ایمیل اطلاع‌رسانی ارسال گردید.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"خطا در رد کاربر: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            user.IsActive = true;
            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "کاربر با موفقیت فعال شد.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            user.IsActive = false;
            await _userManager.UpdateAsync(user);

            TempData["SuccessMessage"] = "کاربر با موفقیت غیرفعال شد.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Users/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _adminService.DeleteUserAsync(id);

            TempData[success ? "SuccessMessage" : "ErrorMessage"] =
                success ? "کاربر با موفقیت حذف شد." : "کاربر یافت نشد یا خطایی رخ داد.";

            return RedirectToAction(nameof(Index));
        }
    }
}