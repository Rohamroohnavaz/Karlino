using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.MainServices
{
    public class AttachService : IAttachService
    {
        private readonly IAttachRepository _attachRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public AttachService(IAttachRepository attachRepository 
            ,IUnitOfWork unitOfWork 
            ,ICurrentUserService currentUserService)
        {
            _attachRepository = attachRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Attach> CreateAttachAsync(UploadAttachDto dto)
        {
            var attach = new Attach(dto.FilePath ,dto.FileName ,dto.ContentType ,dto.FileSize);

            await _attachRepository.AddAsync(attach);
            await _unitOfWork.SaveChangesAsync();

            return attach;
        }

        public async Task DeleteAttachAsync(Guid attachId)
        {
            var attach = await _attachRepository.GetByIdAsync(attachId);

            if (attach is null)
                return;

            await _attachRepository.HardDeleteAsync(attachId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
