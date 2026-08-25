using MyFinalProject.Application.Commands.ViewModels;
using MyFinalProject.Application.DTOs;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Services.ServiceInterfaces
{
    public interface IRequestResumeService
    {
        Task<Guid> CreateResumeRequest(Guid advertisementId, CreateRequestResumeDto dto);

        Task ChangeRequestStatusAsync(ChangeRequestStatusDto dto);

        Task<List<RequestResume>> GetRequestsByAdverIdAsync(Guid advertisementId);

        Task<RequestResume?> GetRequestByUserIdAsync();

        Task<RequestResume?> GetRequestByAttachIdAsync(Guid attachId);

        Task UploadFileAttachAsync(Guid requestId, UploadAttachDto dto);

        Task ReplaceFileAttachAsync(Guid requestId, UploadAttachDto dto);

        Task<RequestResume?> GetRequestResumeWithAttachByCompanyIdAsync(Guid resumeId, Guid companyId);

        Task ChangeStatusAsync(Guid requestId, RequestStatus newStatus , CancellationToken cancellationToken);

        Task SendStatusChangedEmailAsync(RequestResume request, RequestStatus status, CancellationToken cancellationToken);

        Task<List<MyApplicationDto>> GetMyApplicationsAsync(Guid userId);

        Task<bool> ApplyForAdvertisementAsync(Guid userId, Guid advertisementId);

        Task<ResumesDto> GetResumesDtoAsync(Guid userId);

        Task<bool> SaveResumeAsync(Guid userId, ResumesDto viewModel, string? savedFilePath);

        Task<(byte[] FileBytes, string FileName)?> GetResumeFileAsync(Guid userId);

        Task<bool> DeleteResumeAsync(Guid userId);
    }
}
