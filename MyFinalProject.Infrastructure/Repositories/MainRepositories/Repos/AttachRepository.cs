using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.Generics;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class AttachRepository : GenericRepository<Attach>, IAttachRepository
    {
        public AttachRepository(FinalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Attach?> GetByFilePathAsync(string path)
        {
            return await _dbContext.Attaches
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FilePath == path);
        }

        public async Task<Attach?> GetByIdWithTrackingAsync(Guid id)
        {
            return await _dbContext.Attaches
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<List<Attach>> GetByUserIdAsync(Guid userId)
        {
            return await _dbContext.Attaches
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }
    }
}
