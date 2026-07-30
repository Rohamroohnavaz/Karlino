using MyFinalProject.Infrastructure.Repositories.MainRepositories;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinalDbContext _dbContext;

        public UnitOfWork(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
            advertisementRepository = new AdvertisementRepository(dbContext);
        }

        public AdvertisementRepository advertisementRepository { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
