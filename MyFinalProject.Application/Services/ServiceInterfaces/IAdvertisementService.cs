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

        Task<Advertisement?> GetCompanyAdvertisementAsync(Guid adverId);

        Task<List<AdvertisementViewModel>> GetActiveAdvertisementAsync();

        Task<List<AdvertisementViewModel>> SearchAndFilterAdsAsync(AdverSearchFilterDto filter);
    }
}
