using MyFinalProject.Application.Commands.AdverCommands;
using MyFinalProject.Application.Commands.ViewModels;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Filters;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface IAdvertisementService
    {
        Task CreateAdvertisementAsync(CreateAdvertisementDto dto);

        Task<Advertisement?> GetAdvertisementByCompanyIdAsync(Guid companyId);

        Task<CreateAdvertisementDto?> GetAdvertisementByIdAsync(Guid adverId);

        Task<List<AdvertisementViewModel>> GetActiveAdvertisementAsync();

        Task<List<AdvertisementViewModel>> SearchAndFilterAdsAsync(AdverSearchFilterDto filter);

        Task UpdateAdvertisement(UpdateAdvertisementCommand command);

        Task DeleteAdvertisement(DeleteAdvertisementCommand command);

        Task<bool> FeatureAdvertisementAsync(Guid advertisementId, Guid userId, int days);

        Task<bool> UnfeatureAdvertisementAsync(Guid advertisementId, Guid userId);

        //Task<List<Infrastructure.DTO.AdminAdvertisementTableDto>> GetMyAdsAsync(Guid userId);
    }
}
