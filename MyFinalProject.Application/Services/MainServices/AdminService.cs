using Microsoft.AspNetCore.Identity;
using MyFinalProject.Application.DTOs.AdminDTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public AdminService(IUnitOfWork unitOfWork 
            ,UserManager<User> userManager 
            ,IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Task<bool> ApproveEmployerAsync(Guid employerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FeatureAdvertisementAsync(Guid adverId, bool isFeatured, int? days = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminAdvertisementListDto>> GetAllAdvertisementAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AdminDashboardDto> GetDashboardStatsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AdminEmployerDetailsDto> GetEmployerDetailsAsync(Guid employerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminEmployerListDto>> GetEmployersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AdminJobSeekerDetailsDto> GetJobSeekerDetailsAsync(Guid jobSeekerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminJobSeekerListDto>> GetJobSeekersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RejectEmployersAsync(Guid employerId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAdvertisementAsync(Guid adverId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleAdvertisementStatusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ToggleJobSeekerStatusAsync(Guid jobSeekerId, bool isActive)
        {
            throw new NotImplementedException();
        }
    }
}
