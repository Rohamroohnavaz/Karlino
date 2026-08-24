using Microsoft.AspNetCore.Identity;
using MyFinalProject.Application.Constants;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Application.ServiceExceptions;
using MyFinalProject.Application.Services.ServiceInterfaces;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using MyFinalProject.Infrastructure.RepoExceptions;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos;
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
        private readonly UserManager<User> _userManager;
        private readonly IAdvertisementRepository _advertiserRepository;
        private readonly IEmailService _emailService;

        public RequestResumeService(IRequestResumeRepository requestResumeRepository
            , ICurrentUserService currentUserService
            , IUnitOfWork unitOfWork
            , IAttachService attachService
            , UserManager<User> userManager
            , IAdvertisementRepository advertisementRepository
            , IEmailService emailService)
        {
            _requestRepository = requestResumeRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _attachService = attachService;
            _userManager = userManager;
            _advertiserRepository = advertisementRepository;
            _emailService = emailService;
        }

        public async Task<Guid> CreateResumeRequest(Guid advertisementId, CreateRequestResumeDto dto)
        {
            var userId = _currentUserService.UserId;

            var advertisementExists =
                await _advertiserRepository.ExistAdvertisementAsync(advertisementId);

            if (!advertisementExists)
                throw new InvalidRequestResumeException("Advertisement not found !!");

            var duplicate = await _requestRepository.ExistsByUserAndAdvertisement(userId, advertisementId);

            if (duplicate)
                throw new InvalidRequestResumeException(
                    "You have already requested this advertisement !!");

            var request = new RequestResume
                (
                  dto.JobSeekerName,
                  dto.JobSeekerLastName,
                  dto.Province,
                  dto.City,
                  dto.StartDate,
                  dto.ExpireDate,
                  dto.UserId,
                  dto.AdvertisementId,
                  dto.AttachmentId
                );

            await _requestRepository.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return request.Id;
        }

        public async Task ChangeRequestStatusAsync(ChangeRequestStatusDto dto)
        {
            var companyId = _currentUserService.CompanyId;

            var request = await _requestRepository
                .GetRequestResumeByCompanyId(dto.RequestResumeId, companyId);

            if (request is null)
                throw new InvalidRequestResumeException("RequestResume not found !!");

            var checkApprovedUser = await _currentUserService.GetAndEnsureApprovedAsync();

            if (checkApprovedUser is null)
                throw new UserNotFoundException("We can't find approved user :/");

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

            var checkApprovedUser = await _currentUserService.GetAndEnsureApprovedAsync();

            if (checkApprovedUser is null)
                throw new UserNotFoundException("We can't find approved user :/");

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
                throw new PermissionDeniedException();

            if (request.AttachmentId.HasValue)
                throw new PermissionDeniedException();

            // var roleManage = await _userManager.GetRolesAsync(RoleConstants.JobSeekerRole);
            var checkApprovedUser = await _currentUserService.GetAndEnsureApprovedAsync();

            if (checkApprovedUser is null)
                throw new UserNotFoundException("We can't find approved user :/");

            var attach = await _attachService.CreateAttachAsync(dto);

            request.SetAttach(attach.Id, attach);

            await _requestRepository.UpdateAsync(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ReplaceFileAttachAsync(Guid requestId, UploadAttachDto dto)
        {
            var userId = _currentUserService.UserId;
            var request = await _requestRepository.GetRequestWithAttachmentByUserId(requestId, userId);

            if (request is null)
                throw new PermissionDeniedException();

            if (request.Attach is not null)
            {
                await _attachService.DeleteAttachAsync(request.Attach.Id);
                request.AttachmentId = null;
            }

            var newAttach = await _attachService.CreateAttachAsync(dto);

            request.SetAttach(newAttach.Id, newAttach);

            await _requestRepository.UpdateAsync(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<RequestResume?> GetRequestResumeWithAttachByCompanyIdAsync(Guid resumeId, Guid companyId)
        {
            var request = await _requestRepository.GetRequestResumeWithAttachByCompanyId(resumeId, companyId);

            if (request is null)
                throw new InvalidRequestResumeException("Request With Attach Not Found !!");

            return request;
        }

        public async Task ChangeStatusAsync(Guid requestId, 
            RequestStatus newStatus ,CancellationToken cancellationToken)
        {
            var requestResume = await _requestRepository.GetByIdAsync(requestId);
            if (requestResume is null)
                throw new ArgumentException(nameof(requestResume));

            requestResume.SetStatus(newStatus);

            await _requestRepository.UpdateAsync(requestResume);
            await _unitOfWork.SaveChangesAsync();

            await SendStatusChangedEmailAsync(requestResume, newStatus ,cancellationToken);

        }

        public async Task SendStatusChangedEmailAsync(RequestResume request,
            RequestStatus status, CancellationToken cancellationToken)
        {
            var jobSeeker = await _userManager.FindByIdAsync(request.UserId.ToString());
            var advertisement = await _advertiserRepository.GetByIdAsync(request.AdvertisementId.Value);

            if (jobSeeker is null || advertisement is null)
                return;

            string recipientEmail = jobSeeker.Email;
            string subject = "";
            string body = "";

            switch (status)
            {
                case RequestStatus.Pending:
                    subject = $"Your request for this advertisement {advertisement.Title} is pending...";
                    body = $"Hey {jobSeeker.FirstName} ! Your request for advertisement {advertisement.Title} is pending . Please be patient ";
                    break;
                case RequestStatus.CurrentlyViewing:
                    subject = $"Your request for this advertisement {advertisement.Title} is viewing ! ";
                    body = $"Hi {jobSeeker.FirstName} ! Your request for advertisement {advertisement.Title} is viewing . Await the result";
                    break;
                case RequestStatus.Interview:
                    subject = $"Your request for this advertisement {advertisement.Title} accepted for interview !";
                    body = $"Hello {jobSeeker.FirstName} ! Congratulation Your request for advertisement {advertisement.Title} accepted for interview ";
                    break;
                case RequestStatus.Success:
                    subject = $"Your request for this advertisement {advertisement.Title} accepted .";
                    body = $"Hey {jobSeeker.FirstName} ! Your request for advertisement {advertisement.Title} successfuly accepted";
                    break;
                case RequestStatus.Fail:
                    subject = $"Your request for this advertisement {advertisement.Title} failed !";
                    body = $"Hi {jobSeeker.FirstName} ! Unfortunately your request for advertisement {advertisement.Title} cancelled !";
                    break;
                default:
                    return;
            }

            await _emailService.SendEmailAsync(recipientEmail, subject, body, true, cancellationToken);
        }

        public async Task<List<MyApplicationDto>> GetMyApplicationsAsync(Guid userId)
        {
            return await _requestRepository.GetMyApplicationsAsync(userId);
        }

        public async Task<bool> ApplyForAdvertisementAsync(Guid userId, Guid advertisementId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return false;

            var advertisement = await _advertiserRepository.GetByIdAsync(advertisementId);
            if (advertisement == null || !advertisement.IsActive || advertisement.IsDeleted || advertisement.ExpireDate <= DateTime.Now)
                return false;

            var resume = await _requestRepository.GetRequestByUserId(userId);
            if (resume == null) return false;

            try
            {
                var request = new RequestResume(
                    jobSeekerName: user.FirstName ?? "نام",
                    jobSeekerLastName: user.LastName ?? "نام خانوادگی",
                    province: "تهران", // یا از پروفایل کاربر
                    city: resume?.City ?? "تهران",
                    startDate: DateTime.Now,
                    expireDate: DateTime.Now.AddMonths(3),
                    userId: userId,
                    advertisementId: advertisementId,
                    attachmentId: Guid.Empty
                );

                request.ChangeJobSeekerTitle(resume?.Title ?? "درخواست همکاری");
                request.SetAboutMe(resume?.AboutMe ?? "");
                request.SetDescription(resume?.Description ?? "");
                request.SetAddress(resume?.Address ?? "");
                request.Status = MyFinalProject.Domain.Entities.Enums.RequestStatus.Pending;
                request.IsDeleted = false;

                await _requestRepository.AddAsync(request);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
  
