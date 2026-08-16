using MyFinalProject.Application.DTOs.AdminDTOs;
using MyFinalProject.Application.Requests;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminEmployerTableDto = MyFinalProject.Infrastructure.DTO.AdminEmployerTableDto;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface IAdminService
    {
        //Employers
        Task<List<AdminEmployerListDto>> GetEmployersAsync();

        Task<AdminEmployerDetailsDto> GetEmployerDetailsAsync(Guid employerId);

        Task<SendEmailResponse> ApproveEmployerAsync(SendEmailRequest request, Guid employerId 
            ,CancellationToken cancellationToken);

        Task<SendEmailResponse> RejectEmployersAsync(SendEmailRequest request, Guid employerId
            , CancellationToken cancellationToken);

        Task<List<AdminEmployerTableDto>> GetPendingEmployersAsync();

        Task<bool> ApproveEmployerAsync(Guid id);

        Task<bool> RejectEmployerAsync(Guid id);

        /////////////

        ///JobSeekers
        Task<List<AdminJobSeekerListDto>> GetJobSeekersAsync();

        Task<AdminJobSeekerDetailsDto> GetJobSeekerDetailsAsync(Guid jobSeekerId);

        Task<bool> ToggleJobSeekerStatusAsync(Guid jobSeekerId, bool isActive);

        Task<SendEmailResponse> ApproveJobSeekerAsync(SendEmailRequest request, Guid jobSeekerId
            , CancellationToken cancellation);

        Task<SendEmailResponse> RejectJobSeekerAsync(SendEmailRequest request, Guid employerId
            , CancellationToken cancellationToken);

        Task<List<AdminUserTableDto>> GetAllUsersAsync();

        /////////////

        ///Advertisement
        Task<List<AdminAdvertisementListDto>> GetAllAdvertisementAsync();

        Task<bool> ToggleAdvertisementStatusAsync(Guid adverId ,bool isActive);

        Task<bool> FeatureAdvertisementAsync(Guid adverId, bool isFeatured, int? days = null);

        Task<bool> SoftDeleteAdvertisementAsync(Guid adverId);

        Task<List<Infrastructure.DTO.AdminAdvertisementTableDto>> GetLatestJobPostingsAsync(int count = 10);

        Task<bool> SetJobPostingActiveAsync(Guid id, bool isActive);

        /////////////

        ///Dashboard
        Task<AdminDashboardDto> GetDashboardStatsAsync();
    }
}
