using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<Advertisement?> GetAdvertisementByCompanyId(Guid companyId);

        Task<Advertisement?> GetCompanyAdvertisement(Guid adverId);

        Task<Advertisement?> GetAdvertisementWithRequestResume(Guid resumeId);

        Task<bool> ExistAdvertisementAsync(Guid adverId);
    }
}
