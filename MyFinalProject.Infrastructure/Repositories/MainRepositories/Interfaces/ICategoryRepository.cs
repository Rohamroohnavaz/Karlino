using MyFinalProject.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<AdminCategoryDto>> GetAllAsync();
        Task AddAsync(string title ,string description);
        Task<bool> UpdateAsync(Guid id, string title);
        Task<bool> DeleteAsync(Guid id);
    }
}
