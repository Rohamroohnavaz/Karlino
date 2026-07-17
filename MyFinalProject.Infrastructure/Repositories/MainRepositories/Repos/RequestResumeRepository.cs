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
    public class RequestResumeRepository : GenericRepository<RequestResume>, IRequestResumeRepository
    {
        public RequestResumeRepository(FinalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RequestResume?> GetRequestByAdverId(Guid adverId)
        {
            var resume = await _dbContext.Resumes
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.AdvertisementId == adverId);

            if (resume == null)
                throw new InvalidRequestResumeException($"{nameof(resume)} doesn't exist !!");

            return resume;
        }

        public async Task<RequestResume?> GetRequestResumeByCompanyId(Guid resumeId,Guid companyId)
        {
            return await _dbContext.Resumes
                .AsNoTracking()
                .Include(r => r.Advertisement)
                .FirstOrDefaultAsync(r => r.Id == resumeId
                && r.Advertisement.CompanyId == companyId);
        }

        public async Task<RequestResume?> GetRequestWithAdvertisement(Guid requestId)
        {
            return await _dbContext.Resumes
                .AsNoTracking()
                .Include(r => r.Advertisement)
                .FirstOrDefaultAsync(r => r.Id == requestId);
        }
    }
}
