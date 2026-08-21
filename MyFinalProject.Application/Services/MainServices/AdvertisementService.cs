using MyFinalProject.Application.Commands.AdverCommands;
using MyFinalProject.Application.Commands.ViewModels;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Filters;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.RepoExceptions;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos;
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
        private readonly ISettingRepository _settingRepository;

        public AdvertisementService(IAdvertisementRepository advertisementRepository
            , IUnitOfWork unitOfWork
            , ISettingRepository settingRepository)
        {
            _advertisementRepository = advertisementRepository;
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
        }

        public async Task CreateAdvertisementAsync(CreateAdvertisementDto dto)
        {
            var maxSetting = await _settingRepository.GetValueAsync("MaxActiveAdsPerEmployer");
            var max = int.TryParse(maxSetting, out var m) ? m : 10;

            var activeCount = await _advertisementRepository.GetActiveCountByEmployerAsync(dto.CompanyId);

            if (activeCount >= max)
            {
                throw new Exception($"شما به حداکثر تعداد آگهی فعال ({max}) رسیده‌اید.");
            }

            var IsfindAdvertisement = await _advertisementRepository.ExistByTitle(dto.Title);

            if (IsfindAdvertisement)
                throw new InvalidAdvertisementException("This Advertisement Already Exist !!");

            var advertisement = new Advertisement(dto.Title, dto.Description, dto.Salary
                , dto.Province, dto.City, dto.CompanyName, dto.CompanyId);

            await _advertisementRepository.AddAsync(advertisement);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<AdvertisementViewModel>> GetActiveAdvertisementAsync()
        {
            var advertisements = await _advertisementRepository
                .QueryAsync(a => a.IsActive && a.ExpireDate > DateTime.UtcNow);

            return advertisements
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
                   CreatedAt = a.CreatedAt,
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

        public async Task<CreateAdvertisementDto?> GetAdvertisementByIdAsync(Guid adverId)
        {
            var advertisement = await _advertisementRepository.GetCompanyAdvertisement(adverId);

            if (advertisement == null)
                throw new InvalidAdvertisementException("Advertisement not found !!");

            return new CreateAdvertisementDto
            {
                Title = advertisement.Title,
                Description = advertisement.Description,
                Salary = advertisement.Salary,
                CompanyName = advertisement.CompanyName,
                Province = advertisement.Province,
                City = advertisement.City,
            };

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

        public async Task UpdateAdvertisement(UpdateAdvertisementCommand command)
        {
            var advertisement = await _advertisementRepository.GetByIdAsync(command.Id);

            if (advertisement is null)
                throw new ArgumentException("Advertisement Not Found !");

            if (advertisement.CompanyId != command.CompanyId)
                throw new ArgumentException("You don't have permission to edit this advertisement");

            advertisement.ChangeTitle(command.Title);
            advertisement.ChangeDescription(command.Description);
            advertisement.ChangeSalary(command.Salary);
            advertisement.ChangeCompanyName(command.CompanyName);
            advertisement.ChangeProvince(command.Province);
            advertisement.ChangeCity(command.City);

            await _advertisementRepository.UpdateAsync(advertisement);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAdvertisement(DeleteAdvertisementCommand command)
        {
            var advertisement = await _advertisementRepository.GetByIdAsync(command.Id);

            if (advertisement is null)
                throw new ArgumentException("Advertisement Not Found !");

            await _advertisementRepository.HardDeleteAsync(command.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
