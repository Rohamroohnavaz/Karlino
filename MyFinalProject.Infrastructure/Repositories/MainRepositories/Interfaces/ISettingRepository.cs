using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface ISettingRepository
    {
        Task<List<Setting>> GetAllAsync();
        Task SetValueAsync(string key, string value);
        Task SeedDefaultsAsync();
        Task<string?> GetValueAsync(string key);
    }
}
