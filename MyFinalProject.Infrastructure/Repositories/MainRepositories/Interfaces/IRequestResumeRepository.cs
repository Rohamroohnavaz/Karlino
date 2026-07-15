using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IRequestResumeRepository : IGenericRepository<RequestResume>
    {
        Task<RequestResume?> GetRequestByAdverId(Guid adverId);

        Task<RequestResume?> GetRequestWithAdvertisement(Guid requestId);
    }
}
