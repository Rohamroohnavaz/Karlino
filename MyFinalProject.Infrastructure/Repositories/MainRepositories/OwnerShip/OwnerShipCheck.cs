using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.OwnerShip
{
    public class OwnerShipCheck : IOwnerShipCheck
    {
        private readonly FinalDbContext _dbContext;

        public OwnerShipCheck(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsOwnerShipOfAdvertisement(Guid advertisementId, Guid companyId)
        {
            return await _dbContext.Advertisements
                .AnyAsync(a => a.Id == advertisementId && a.CompanyId == companyId);
        }
    }
}
