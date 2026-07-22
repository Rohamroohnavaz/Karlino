using MyFinalProject.Application.DTOs;
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
        Task CreateAdvertisement(CreateAdvertisementDto dto);

        Task<Advertisement?> GetAdvertisementByCompanyIdAsync(Guid companyId);

        Task<Advertisement?> GetCompanyAdvertisement(Guid adverId);
    }
}
