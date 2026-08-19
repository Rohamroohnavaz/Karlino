using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.RepoExceptions;
using MyFinalProject.Infrastructure.Repositories.Generics;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(FinalDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistAdvertisementAsync(Guid adverId)
        {
            return await _dbContext.Advertisements
                .AnyAsync(a => a.Id == adverId);
        }

        public async Task<bool> ExistByTitle(string title)
        {
            return await _dbContext.Advertisements
                .AnyAsync(a => a.Title == title);
        }

        public async Task<int> GetActiveCountByEmployerAsync(Guid employerId)
        {
            return await _dbContext.Advertisements
                .CountAsync(a => a.CompanyId == employerId && a.IsActive == true);
        }

        public async Task<Advertisement?> GetAdvertisementByCompanyId(Guid companyId)
        {
            var advertisement = await _dbContext.Advertisements
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.CompanyId == companyId);

            if (advertisement is null)
                throw new InvalidAdvertisementException($"{nameof(advertisement)} doesn't exist !!");

            return advertisement;
        }

        public async Task<List<Advertisement>> GetAllWithSoftDelete()
        {
            return await _dbContext.Advertisements
                .IgnoreQueryFilters()
                .Include(a => a.Company)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        //public async Task<Advertisement?> GetAdvertisementWithRequestResume(Guid resumeId)
        //{
        //    return await _dbContext.Advertisements
        //        .AsNoTracking()
        //        .Include(a => a.RequestResumes)
        //        .FirstOrDefaultAsync(a => a.RequestResumeId == resumeId);
        //}

        public async Task<Advertisement?> GetCompanyAdvertisement(Guid adverId)
        {
            return await _dbContext.Advertisements
                .AsNoTracking()
                .Include(a => a.Company)
                .FirstOrDefaultAsync(a => a.Id == adverId);
        }

        public async Task<Guid?> GetCompanyIdByUserEmailAsync(string email)
        {
            return await _dbContext.Companies
                .Where(c => c.User.Email == email)
                .Select(c => (Guid?)c.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountByStatus(bool isActive)
        {
            return await _dbContext.Advertisements
                .CountAsync(a => a.IsActive == isActive);
        }

        public async Task<AdminAdvertisementDetailsDto?> GetDetailsForAdminAsync(Guid id)
        {
            return await _dbContext.Advertisements
                .Where(a => a.Id == id)
                .Select(a => new AdminAdvertisementDetailsDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    EmployerName = a.User.FirstName + " " + a.User.LastName,  
                    EmployerEmail = a.User.Email,                                  
                    CityName = a.City,                                           
                    CategoryTitle = a.Category.CategoryName,                                
                    CreatedAt = a.CreatedAt,
                    IsActive = a.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<AdminAdvertisementTableDto>> GetLatestForAdminAsync(int count)
        {
            return await _dbContext.Advertisements
                .OrderByDescending(a => a.CreatedAt)
                .Take(count)
                .Select(a => new AdminAdvertisementTableDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    EmployerName = a.CompanyName,
                    CityName = a.City,
                    CreatedAt = a.CreatedAt,
                    IsActive = a.IsActive
                }).ToListAsync();
        }

        public async Task<List<AdminAdvertisementTableDto>> GetMyAdsAsync(string email)
        {
            return await _dbContext.Advertisements
                .Where(a => a.Company.User.Email == email)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AdminAdvertisementTableDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    EmployerName = a.CompanyName,           
                    CityName = a.City,                  
                    CreatedAt = a.CreatedAt,
                    IsActive = a.IsActive
                })
                .ToListAsync();
        }

        public async Task<(List<AdminAdvertisementTableDto>, int)> GetPagedForAdminAsync(
             string? search,bool? isActive, int page,int pageSize)
        {
            var query = _dbContext.Advertisements.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a => a.Title.Contains(search));
            }

            if (isActive.HasValue)
            {
                query = query.Where(a => a.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AdminAdvertisementTableDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    EmployerName = a.CompanyName,  
                    CityName = a.City,
                    CreatedAt = a.CreatedAt,
                    IsActive = a.IsActive
                })
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> IsOwnerAsync(Guid advertisementId, string email)
        {
            return await _dbContext.Advertisements
                .AnyAsync(a => a.Id == advertisementId
                            && a.Company.User.Email == email); 
        }
    }
}
