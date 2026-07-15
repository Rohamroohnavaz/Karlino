using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.Generics;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories
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
    }
}
