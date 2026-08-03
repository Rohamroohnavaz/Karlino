using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.DTOs.AdminDTOs;
using MyFinalProject.Application.Requests;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Application.Services.Settings;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IRequestResumeRepository _requestResumeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly EmailSetting _emailSetting;

        public AdminService(IUnitOfWork unitOfWork
            , UserManager<User> userManager
            , IEmailService emailService
            , IUserRepository userRepository
            , IOptions<EmailSetting> emailSetting
            , IAdvertisementRepository advertisementRepository
            , IRequestResumeRepository requestResumeRepository
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailService = emailService;
            _userRepository = userRepository;
            _emailSetting = emailSetting.Value;
            _advertisementRepository = advertisementRepository;
            _requestResumeRepository = requestResumeRepository;
        }

        #region Employer Implementation

        public async Task<List<AdminEmployerListDto>> GetEmployersAsync()
        {
            var employers = await _userRepository.GetUsersByRole(RoleConstants.EmployerRole.ToString());

            return employers.Select(u => new AdminEmployerListDto
            {
                Id = u.Id,
                FullName = $"{u.FirstName} {u.LastName}",
                Email = u.Email,
                CompanyName = u.Company?.CompanyName ?? "No Registered Company !",
                IsApproved = u.IsApproved
            }).ToList();
        }

        public async Task<AdminEmployerDetailsDto> GetEmployerDetailsAsync(Guid employerId)
        {
            var user = await _userRepository.GetUserWithCompany(employerId);
            if (user == null || user.Role != UserRole.Employer)
                throw new KeyNotFoundException("Employer Not Found !!");

            return new AdminEmployerDetailsDto
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsApproved = user.IsApproved,
                Company = user.Company == null ? null : new CompanyDto
                {
                    Id = user.Company.Id,
                    CompanyName = user.Company.CompanyName,
                    CompanyLocation = user.Company.CompanyLocation,
                    Province = user.Company.Province,
                    City = user.Company.City
                }
            };
        }

        public async Task<SendEmailResponse> ApproveEmployerAsync(SendEmailRequest request,
            Guid employerId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(employerId.ToString());
            if (user == null || user.Role != UserRole.Employer)
                throw new KeyNotFoundException("Employer Not Found !!");

            if (user.IsApproved)
                throw new InvalidOperationException("Employer already approved !!");

            user.IsApproved = true;

            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            try
            {
                var isHtml = request.isHtml ?? _emailSetting.DefaultHtml;

                await _emailService.SendEmailAsync(request.To, request.Subject, request.Body, isHtml, cancellationToken);

                return new SendEmailResponse
                {
                    Success = true,
                    Message = "Send"
                };
            }
            catch (Exception ex)
            {
                return new SendEmailResponse
                {
                    Success = false,
                    Message = $"It was not approved ! {ex.Message}"
                };
            }
        }

        public async Task<SendEmailResponse> RejectEmployersAsync(SendEmailRequest request,
            Guid employerId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(employerId.ToString());
            if (user == null || user.Role != UserRole.Employer)
                throw new KeyNotFoundException("Employer Not Found !!");

            user.IsApproved = false;

            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            try
            {
                var isHtml = request.isHtml ?? _emailSetting.DefaultHtml;

                await _emailService.SendEmailAsync(request.To, request.Subject, request.Body, isHtml, cancellationToken);

                return new SendEmailResponse
                {
                    Success = true,
                    Message = "Send"
                };
            }
            catch (Exception ex)
            {
                return new SendEmailResponse
                {
                    Success = false,
                    Message = $"It was not rejected !{ex.Message}"
                };
                
            }
        }

        #endregion

        #region JobSeeker Implementation

        public async Task<AdminJobSeekerDetailsDto> GetJobSeekerDetailsAsync(Guid jobSeekerId)
        {
            var user = await _userRepository.GetUserByResumes(jobSeekerId);
            if (user == null || user.Role != UserRole.JobSeeker)
                throw new KeyNotFoundException("JobSeeker Not Found !!");

            return new AdminJobSeekerDetailsDto
            {
                Id = jobSeekerId,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                Resumes = user.RequestResumes.Select(r => new ResumeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description
                }).ToList()
            };
        }

        public async Task<List<AdminJobSeekerListDto>> GetJobSeekersAsync()
        {
            var jobSeekers = await _userRepository.GetUsersByRole(UserRole.JobSeeker.ToString());

            return jobSeekers.Select(j => new AdminJobSeekerListDto
            {
                Id = j.Id,
                FullName = $"{j.FirstName} {j.LastName}",
                Email = j.Email,
                IsActive = j.IsActive
            }).ToList();
        }

        public async Task<bool> ToggleJobSeekerStatusAsync(Guid jobSeekerId, bool isActive)
        {
            var user = await _userManager.FindByIdAsync(jobSeekerId.ToString());
            if (user == null || user.Role != UserRole.JobSeeker)
                throw new KeyNotFoundException("JobSeeker Not Found !");

            user.IsActive = isActive;
            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<SendEmailResponse> ApproveJobSeekerAsync(SendEmailRequest request, Guid jobSeekerId
            , CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(jobSeekerId.ToString());
            if (user is null || user.Role != UserRole.JobSeeker)
                throw new KeyNotFoundException("JobSeeker Not Found !");

            if (user.IsApproved)
                throw new InvalidOperationException("Jobseeker already approved !");

            user.IsApproved = true;

            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            try
            {
                var isHtml = request.isHtml ?? _emailSetting.DefaultHtml;

                await _emailService.SendEmailAsync(request.To, request.Subject, request.Body, isHtml, cancellationToken);

                return new SendEmailResponse
                {
                    Success = true,
                    Message = "Send"
                };
            }
            catch (Exception ex)
            {
                return new SendEmailResponse
                {
                    Success = false,
                    Message = $"It was not approved ! {ex.Message}"
                };
            }

        }

        public async Task<SendEmailResponse> RejectJobSeekerAsync(SendEmailRequest request, Guid jobSeekerId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(jobSeekerId.ToString());
            if (user == null || user.Role != UserRole.Employer)
                throw new KeyNotFoundException("Employer Not Found !!");

            user.IsApproved = false;

            await _userManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            try
            {
                var isHtml = request.isHtml ?? _emailSetting.DefaultHtml;

                await _emailService.SendEmailAsync(request.To, request.Subject, request.Body, isHtml, cancellationToken);

                return new SendEmailResponse
                {
                    Success = true,
                    Message = "Send"
                };
            }
            catch (Exception ex)
            {
                return new SendEmailResponse
                {
                    Success = false,
                    Message = $"It was not rejected !{ex.Message}"
                };

            }
        }

        #endregion

        #region Advertisement Implementation

        public async Task<List<AdminAdvertisementListDto>> GetAllAdvertisementAsync()
        {
            var advertisements = await _advertisementRepository.GetAllWithSoftDelete();

            return advertisements.Select(a => new AdminAdvertisementListDto
            {
                Id = a.Id,
                Title = a.Title,
                CompanyName = a.Company?.CompanyName ?? "",
                IsActive = a.IsActive,
                IsFeatured = a.IsFeatured,
                FeaturedUntil = a.FeaturedUntil,
                CreatedAt = a.CreatedAt
            }).ToList();
        }

        public async Task<bool> FeatureAdvertisementAsync(Guid adverId, bool isFeatured, int? days = null)
        {
            var adver = await _advertisementRepository.GetByIdAsync(adverId);
            if (adver == null)
                throw new KeyNotFoundException("Advertisement Not Found !");

            adver.IsFeatured = isFeatured;

            if (isFeatured)
            {
                adver.MakeFeatured(7);
            }
            else
            {
                adver.CancelFeature();
            }

            await _unitOfWork.advertisementRepository.UpdateAsync(adver);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAdvertisementAsync(Guid adverId)
        {
            var adver = await _advertisementRepository.GetByIdAsync(adverId);
            if (adver == null)
                throw new KeyNotFoundException("Advertisement Not Found !!");

            adver.IsDeleted = true;

            await _unitOfWork.advertisementRepository.UpdateAsync(adver);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleAdvertisementStatusAsync(Guid adverId, bool isActive)
        {
            var adver = await _advertisementRepository.GetByIdAsync(adverId);
            if (adver == null)
                throw new KeyNotFoundException("Job Advertisement Not Found !!");

            adver.IsActive = isActive;

            await _unitOfWork.advertisementRepository.UpdateAsync(adver);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion

        //Dashboard Implementation

        public async Task<AdminDashboardDto> GetDashboardStatsAsync()
        {
            var dashboard = new AdminDashboardDto
            {
                TotalJobSeekers = await _userRepository.GetCountByRole(RoleConstants.JobSeekerRole),
                TotalEmployers = await _userRepository.GetCountByRole(RoleConstants.EmployerRole),
                ActiveJobPostings = await _advertisementRepository.GetCountByStatus(isActive: true),
                InactiveJobPostings = await _advertisementRepository.GetCountByStatus(isActive: false),
                PendingEmployersCount = await _userRepository.GetPendingEmployersCount()
            };

            var requestStats = await _requestResumeRepository.GetStatusStats();
            foreach (var stat in requestStats)
            {
                dashboard.RequestResumeStats[stat.Key.ToString()] = stat.Value;
            }

            return dashboard;
        }

        
    }
}
