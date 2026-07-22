using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.ServiceExceptions;
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
    public class RequestResumeService : IRequestResumeService
    {
        private readonly IRequestResumeRepository _requestRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachService _attachService;

        public RequestResumeService(IRequestResumeRepository requestResumeRepository
            , ICurrentUserService currentUserService
            , IUnitOfWork unitOfWork
            , IAttachService attachService)
        {
            _requestRepository = requestResumeRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _attachService = attachService;
        }

        public async Task ChangeRequestStatusAsync(ChangeRequestStatusDto dto)
        {
            var companyId = _currentUserService.CompanyId;

            var request = await _requestRepository
                .GetRequestResumeByCompanyId(dto.RequestResumeId, companyId);

            if (request is null)
                throw new InvalidRequestResumeException("RequestResume not found !!");

            request.Status = dto.Status;

            await _requestRepository.UpdateAsync(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<RequestResume?> GetRequestByAttachIdAsync(Guid attachId)
        {
            var request = await _requestRepository.GetResumeByAttachId(attachId);

            if (request is null)
                throw new InvalidRequestResumeException("Your Request Not Found !!");

            return request;
        }

        public async Task<RequestResume?> GetRequestByUserIdAsync()
        {
            var userId = _currentUserService.UserId;
            var findRequest = await _requestRepository.GetRequestByUserId(userId);

            if (findRequest is null)
                throw new InvalidRequestResumeException("Request Not Found !!");

            return findRequest;
        }

        public async Task<List<RequestResume>> GetRequestsByAdverIdAsync(Guid advertisementId)
        {
            var requests = await _requestRepository.GetRequestByAdverId(advertisementId);

            if (requests.Count == 0)
                return null;

            return requests;
        }

        public async Task UploadFileAttachAsync(Guid requestId, UploadAttachDto dto)
        {
            var userId = _currentUserService.UserId;

            var request = await _requestRepository.GetRequestWithAttachmentByUserId(requestId, userId);

            if (request is null)
                throw new PermissionDeniedException("Request is required !");

            if (request.AttachmentId.HasValue)
                throw new Exception("");

            var attach = await _attachService.CreateAttachAsync(dto);

            request.SetAttach(attach.Id, attach);

            await _requestRepository.UpdateAsync(request);
        }

        public async Task ReplaceFileAttachAsync(Guid requestId, UploadAttachDto dto)
        {
            var userId = _currentUserService.UserId;
            var request = await _requestRepository.GetRequestWithAttachmentByUserId(requestId, userId);

            if (request is null)
                throw new PermissionDeniedException("Request not found !!");

            if (request.Attach is not null)
            {
                await _attachService.DeleteAttachAsync(request.Attach.Id);
                request.AttachmentId = null;
            }

            var newAttach = await _attachService.CreateAttachAsync(dto);

            request.SetAttach(newAttach.Id, newAttach);

            await _requestRepository.UpdateAsync(request);
        }

        public async Task<RequestResume?> GetRequestResumeWithAttachByCompanyIdAsync(Guid resumeId, Guid companyId)
        {
            var request = await _requestRepository.GetRequestResumeWithAttachByCompanyId(resumeId, companyId);

            if (request is null)
                throw new InvalidRequestResumeException("Request With Attach Not Found !!");

            return request;
        }
    }
}
