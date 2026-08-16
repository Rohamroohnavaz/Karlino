using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Persistence;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FinalDbContext _db;

        public CategoryRepository(FinalDbContext db)
        {
            _db = db;
        }

        public async Task<List<AdminCategoryDto>> GetAllAsync()
        {
            return await _db.Categories
                .OrderBy(c => c.CategoryName)
                .Select(c => new AdminCategoryDto
                {
                    Id = c.Id,
                    Title = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();
        }

        public async Task AddAsync(string title ,string description)
        {
            _db.Categories.Add(new Category(title ,description));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, string title)
        {
            var category = await _db.Categories.FindAsync(id);

            if (category == null)
                return false;

            category.SetCategoryName(title);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var category = await _db.Categories.FindAsync(id);

            if (category == null)
                return false;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}