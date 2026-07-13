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
    public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(FinalDbContext dbContext) : base(dbContext)
        {
        }
    }
}
