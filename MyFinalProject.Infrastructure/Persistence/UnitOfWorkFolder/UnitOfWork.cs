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

            Companies = new CompanyRepository(dbContext);
            Advertisements = new AdvertisementRepository(dbContext);
            RequestResumes = new RequestResumeRepository(dbContext);
        }

        public ICompanyRepository Companies { get; set; }

        public IAdvertisementRepository Advertisements { get; set; }

        public IRequestResumeRepository RequestResumes { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
