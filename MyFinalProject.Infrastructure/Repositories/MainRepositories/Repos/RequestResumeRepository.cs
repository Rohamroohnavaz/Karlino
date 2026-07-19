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

        public async Task<List<RequestResume>> GetRequestByAdverId(Guid adverId)
        {
            var requests = await _dbContext.Resumes
               .AsNoTracking()
               .Where(r => r.AdvertisementId == adverId)
               .ToListAsync();

            if (requests == null)
                throw new InvalidRequestResumeException($"{nameof(requests)} doesn't exist !!");

            return requests;
        }

        public async Task<RequestResume?> GetRequestResumeByCompanyId(Guid resumeId, Guid companyId)
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
