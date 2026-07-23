using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IAttachRepository : IGenericRepository<Attach>
    {
        Task<Attach?> GetByFilePathAsync(string path);

        Task<List<Attach>> GetByUserIdAsync(Guid userId);

        Task<Attach?> GetByIdWithTrackingAsync(Guid id);
    }
}
