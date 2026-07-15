using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.RepoExceptions;
using MyFinalProject.Infrastructure.Repositories.Generics;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(FinalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Advertisement?> GetAdvertisementByCompanyId(Guid companyId)
        {
            var advertisement = await _dbContext.Advertisements
                .AsNoTracking().FirstOrDefaultAsync(a => a.CompanyId == companyId);

            if (advertisement is null)
                throw new InvalidAdvertisementException($"{nameof(advertisement)} doesn't exist !!");

            return advertisement;
        }

        public async Task<Advertisement?> GetCompanyAdvertisement(Guid adverId)
        {
            return await _dbContext.Advertisements
                .AsNoTracking()
                .Include(a => a.Company)
                .FirstOrDefaultAsync(a => a.Id == adverId);
        }
    }
}
