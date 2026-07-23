using MyFinalProject.Application.Commands.ViewModels;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Filters;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.RepoExceptions;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyFinalProject.Application.Services.MainServices
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdvertisementService(IAdvertisementRepository advertisementRepository 
            ,IUnitOfWork unitOfWork)
        {
            _advertisementRepository = advertisementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAdvertisementAsync(CreateAdvertisementDto dto)
        {
            var IsfindAdvertisement = await _advertisementRepository.ExistByTitle(dto.Title);

            if (IsfindAdvertisement)
                throw new InvalidAdvertisementException("This Advertisement Already Exist !!");

            var advertisement = new Advertisement(dto.Title , dto.Description ,dto.Salary 
                ,dto.Province ,dto.City ,dto.CompanyName ,dto.CompanyId ,dto.CategoryId);

            await _advertisementRepository.AddAsync(advertisement);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<AdvertisementViewModel>> GetActiveAdvertisementAsync()
        {
            var advertisements = await _advertisementRepository.QueryAsync(a => true);

            return advertisements
               .Where(a => a.IsActive)
               .OrderByDescending(a => a.CreatedAt)
               .Select(a => new AdvertisementViewModel
               {
                   Id = a.Id,
                   Title = a.Title,
                   Description = a.Description,
                   Salary = a.Salary,
                   CompanyName = a.CompanyName,
                   Province = a.Province,
                   City = a.City,
                   CreatedAt = a.CreatedAt
               })
               .ToList();
        }

        public async Task<Advertisement?> GetAdvertisementByCompanyIdAsync(Guid companyId)
        {
            var advertisement = await _advertisementRepository.GetAdvertisementByCompanyId(companyId);

            if (advertisement == null)
                throw new InvalidAdvertisementException("Advertisement Not Found !!");

            return advertisement;
        }

        public async Task<Advertisement?> GetCompanyAdvertisementAsync(Guid adverId)
        {
            var advertisement = await _advertisementRepository.GetCompanyAdvertisement(adverId);

            if (advertisement == null)
                throw new InvalidAdvertisementException("Advertisement not found !!");

            return advertisement;
        }

        public async Task<List<AdvertisementViewModel>> SearchAndFilterAdsAsync(AdverSearchFilterDto filter)
        {
            var query = (await _advertisementRepository.QueryAsync(ad => true))
                .Where(a => a.IsActive);

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(a => a.Title.Contains(filter.SearchTerm)
                || a.City.Contains(filter.SearchTerm));              
            }

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == filter.CategoryId);
            }

            if (filter.MinSalary.HasValue)
            {
                query = query.Where(a => a.Salary >= filter.MinSalary);
            }

            return query.Select(a => new AdvertisementViewModel
            {
                Id = a.Id,
                Title = a.Title,
                CompanyName = a.CompanyName,
                City = a.City,
                Salary = a.Salary,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
    }
}
