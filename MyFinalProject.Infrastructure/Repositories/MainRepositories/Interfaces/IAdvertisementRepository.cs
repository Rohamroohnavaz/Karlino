using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IAdvertisementRepository : IGenericRepository<Advertisement>
    {
        Task<Advertisement?> GetAdvertisementByCompanyId(Guid companyId);

        Task<Advertisement?> GetCompanyAdvertisement(Guid adverId);

        Task<bool> ExistAdvertisementAsync(Guid adverId);

        Task<bool> ExistByTitle(string title);

        Task<List<Advertisement>> GetAllWithSoftDelete();

        Task<int> GetCountByStatus(bool isActive);

        Task<List<AdminAdvertisementTableDto>> GetLatestForAdminAsync(int count);

        Task<(List<AdminAdvertisementTableDto> Items, int TotalCount)> GetPagedForAdminAsync(
            string? search,bool? isActive,int page,int pageSize);

        Task<AdminAdvertisementDetailsDto?> GetDetailsForAdminAsync(Guid id);

        Task<int> GetActiveCountByEmployerAsync(Guid employerId);

        Task<List<AdminAdvertisementTableDto>> GetMyAdsAsync(string email);

        Task<bool> IsOwnerAsync(Guid advertisementId, string email);

        Task<Guid?> GetCompanyIdByUserEmailAsync(string email);

        Task<List<AdminAdvertisementTableDto>> GetByUserIdAsync(Guid userId);

        Task<Advertisement> GetByIdWithCompanyAsync(Guid id);
    }
}
