using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class SettingRepository : ISettingRepository
    {
        private readonly FinalDbContext _dbContext;

        public SettingRepository(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Setting>> GetAllAsync()
        {
            return await _dbContext.Settings.OrderBy(s => s.Key).ToListAsync();
        }

        public async Task SetValueAsync(string key, string value)
        {
            var setting = await _dbContext.Settings.FirstOrDefaultAsync(s => s.Key == key);

            if (setting == null)
            {
                _dbContext.Settings.Add(new Setting(key, value));
            }
            else
            {
                setting.UpdateValue(value);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task SeedDefaultsAsync()
        {
            if (await _dbContext.Settings.AnyAsync())
                return;

            var defaults = new List<Setting>
        {
            new Setting("SiteName", "کاریابی آنلاین"),
            new Setting("SiteDescription", "پلتفرم کاریابی و استخدام"),
            new Setting("ContactEmail", "support@site.com"),
            new Setting("IsRegistrationOpen", "true"),
            new Setting("MaxActiveAdsPerEmployer", "10"),
        };

            _dbContext.Settings.AddRange(defaults);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetValueAsync(string key)
        {
            return await _dbContext.Settings
                .Where(s => s.Key == key)
                .Select(s => s.Value)
                .FirstOrDefaultAsync();
        }
    }
}
