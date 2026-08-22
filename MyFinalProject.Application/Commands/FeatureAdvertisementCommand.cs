using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos;
using System;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Commands
{
    public class FeatureAdvertisementCommand
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FeatureAdvertisementCommand(IAdvertisementRepository advertisementRepository
            , IUnitOfWork unitOfWork)
        {
            _advertisementRepository = advertisementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Execute(Guid advertisementId, Guid userId, int days)
        {
            var ad = await _advertisementRepository.GetByIdAsync(advertisementId);

            if (ad == null || ad.Company?.UserId != userId)
                return false;

            ad.MakeFeatured(10);

            await _advertisementRepository.UpdateAsync(ad);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}