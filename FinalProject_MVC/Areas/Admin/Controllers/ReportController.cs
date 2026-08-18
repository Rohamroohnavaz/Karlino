using FinalProject_MVC.Areas.Admin.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;

namespace FinalProject_MVC.Areas.Admin.Controllers
{
    public class ReportsController : AdminBaseController
    {
        private readonly IReportRepository _reportRepository;
        private readonly IAdminService _adminService;

        public ReportsController(IReportRepository reportRepository, IAdminService adminService)
        {
            _reportRepository = reportRepository;
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "گزارش‌های تخلف";

            var model = await _reportRepository.GetAllAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectAd(Guid reportId, Guid advertisementId)
        {
            await _adminService.SetJobPostingActiveAsync(advertisementId, false);
            await _reportRepository.ChangeStatusAsync(reportId, ReportStatus.Reviewed);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dismiss(Guid reportId)
        {
            await _reportRepository.ChangeStatusAsync(reportId, ReportStatus.Dismissed);

            return RedirectToAction(nameof(Index));
        }
    }
}