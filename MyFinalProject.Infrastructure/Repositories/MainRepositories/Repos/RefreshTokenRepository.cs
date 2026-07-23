using MyFinalProject.Domain.Entities;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>
    {
        protected RefreshTokenRepository(FinalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
