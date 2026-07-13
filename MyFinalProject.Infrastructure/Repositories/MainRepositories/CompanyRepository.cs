using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Domain.Interfaces;
using MyFinalProject.Infrastructure.Repositories.Generics;
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
    }
}
