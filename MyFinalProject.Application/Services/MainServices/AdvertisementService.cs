using MyFinalProject.Application.DTOs;
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

        public async Task CreateAdvertisement(CreateAdvertisementDto dto)
        {
            var IsfindAdvertisement = await _advertisementRepository.ExistByTitle(dto.Title);

            if (IsfindAdvertisement)
                throw new InvalidAdvertisementException("This Advertisement Already Exist !!");

            var advertisement = new Advertisement(dto.Title , dto.Description ,dto.Salary 
                ,dto.Province ,dto.City ,dto.CompanyName ,dto.CompanyId ,dto.CategoryId);

            await _advertisementRepository.AddAsync(advertisement);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Advertisement?> GetAdvertisementByCompanyIdAsync(Guid companyId)
        {
            var advertisement = await _advertisementRepository.GetAdvertisementByCompanyId(companyId);

            if (advertisement == null)
                throw new InvalidAdvertisementException("Advertisement Not Found !!");

            return advertisement;
        }

        public async Task<Advertisement?> GetCompanyAdvertisement(Guid adverId)
        {
            var advertisement = await _advertisementRepository.GetCompanyAdvertisement(adverId);

            if (advertisement == null)
                throw new InvalidAdvertisementException("Advertisement not found !!");

            return advertisement;
        }
    }
}
