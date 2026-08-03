using MyFinalProject.Application.DTOs.AdminDTOs;
using MyFinalProject.Application.Requests;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /////////////

        ///JobSeekers
        Task<List<AdminJobSeekerListDto>> GetJobSeekersAsync();

        Task<AdminJobSeekerDetailsDto> GetJobSeekerDetailsAsync(Guid jobSeekerId);

        Task<bool> ToggleJobSeekerStatusAsync(Guid jobSeekerId, bool isActive);

        Task<SendEmailResponse> ApproveJobSeekerAsync(SendEmailRequest request, Guid jobSeekerId
            , CancellationToken cancellation);

        Task<SendEmailResponse> RejectJobSeekerAsync(SendEmailRequest request, Guid employerId
            , CancellationToken cancellationToken);

        /////////////

        ///Advertisement
        Task<List<AdminAdvertisementListDto>> GetAllAdvertisementAsync();

        Task<bool> ToggleAdvertisementStatusAsync(Guid adverId ,bool isActive);

        Task<bool> FeatureAdvertisementAsync(Guid adverId, bool isFeatured, int? days = null);

        Task<bool> SoftDeleteAdvertisementAsync(Guid adverId);

        /////////////

        ///Dashboard
        Task<AdminDashboardDto> GetDashboardStatsAsync();
    }
}
