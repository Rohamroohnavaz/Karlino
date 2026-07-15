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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(FinalDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<Company>> GetAllExistCompanies()
        {
            return await _dbContext.Companies
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Company?> GetCompanyByUserId(Guid userId)
        {
            var company = await _dbContext.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if(company == null)
                throw new InvalidCompanyException($"{nameof(company)} doesn't exist !!");

            return company;
        }
    }
}
