using MyFinalProject.Domain.Entities.MainModels;
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

        //Task<Advertisement?> GetAdvertisementWithRequestResume(Guid resumeId);

        Task<bool> ExistAdvertisementAsync(Guid adverId);

        Task<bool> ExistByTitle(string title);

        Task<List<Advertisement>> GetAllWithSoftDelete();

        Task<int> GetCountByStatus(bool isActive);
    }
}
