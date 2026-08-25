using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinalProject_MVC.Areas.JobSeeker.ViewModels;
using MyFinalProject.Infrastructure;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.Services.ServiceInterfaces;

namespace FinalProject_MVC.Areas.JobSeeker.Controllers
{
    [Area("JobSeeker")]
    [Authorize(Roles = RoleConstants.JobSeekerRole)]
    public class MyApplicationsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IRequestResumeService _requestResumeService;

        public MyApplicationsController(UserManager<User> userManager
            , IRequestResumeService requestResumeService)
        {
            _userManager = userManager;
            _requestResumeService = requestResumeService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null) return NotFound();

            var dtos = await _requestResumeService.GetMyApplicationsAsync(user.Id);

            var viewModel = dtos.Select(dto => new MyApplicationsViewModel
            {
                Id = dto.Id,
                JobTitle = dto.JobTitle,
                CompanyName = dto.CompanyName,
                City = dto.City,
                AppliedDate = dto.AppliedDate,
                Status = dto.Status.ToString(),
                StatusText = GetStatusText(dto.Status),
                StatusBadgeClass = GetStatusBadgeClass(dto.Status),
                AdvertisementId = dto.AdvertisementId
            }).ToList();

            ViewData["Title"] = "درخواست‌های من";
            return View(viewModel);
        }


        private string GetStatusText(MyFinalProject.Domain.Entities.Enums.RequestStatus status)
        {
            return status switch
            {
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Pending => "در انتظار بررسی",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.CurrentlyViewing => "در حال بررسی",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Interview => "دعوت به مصاحبه",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Success => "پذیرفته شده",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Fail => "رد شده",
                _ => "نامشخص"
            };
        }

        private string GetStatusBadgeClass(MyFinalProject.Domain.Entities.Enums.RequestStatus status)
        {
            return status switch
            {
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Pending => "bg-secondary",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.CurrentlyViewing => "bg-warning text-dark",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Interview => "bg-warning text-dark",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Success => "bg-success",
                MyFinalProject.Domain.Entities.Enums.RequestStatus.Fail => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
}