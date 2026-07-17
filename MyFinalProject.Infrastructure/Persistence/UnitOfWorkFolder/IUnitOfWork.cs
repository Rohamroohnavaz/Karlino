using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder
{
    public interface IUnitOfWork
    {
        ICompanyRepository Companies { get; }

        IAdvertisementRepository Advertisements { get; }

        Task<int> SaveChangesAsync();
    }
}
