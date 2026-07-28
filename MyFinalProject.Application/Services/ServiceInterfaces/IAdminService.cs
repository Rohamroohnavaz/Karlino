using MyFinalProject.Application.DTOs.AdminDTOs;
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

        Task<bool> ApproveEmployerAsync(Guid employerId);

        Task<bool> RejectEmployersAsync(Guid employerId);

        /////////////

        ///JobSeekers
        Task<List<AdminJobSeekerListDto>> GetJobSeekersAsync();

        Task<AdminJobSeekerDetailsDto> GetJobSeekerDetailsAsync(Guid jobSeekerId);

        Task<bool> ToggleJobSeekerStatusAsync(Guid jobSeekerId ,bool isActive);

        /////////////

        ///Advertisement
        Task<List<AdminAdvertisementListDto>> GetAllAdvertisementAsync();

        Task<bool> ToggleAdvertisementStatusAsync();

        Task<bool> FeatureAdvertisementAsync(Guid adverId ,bool isFeatured ,int? days = null);

        Task<bool> SoftDeleteAdvertisementAsync(Guid adverId);

        /////////////

        ///Dashboard
        Task<AdminDashboardDto> GetDashboardStatsAsync();
    }
}
