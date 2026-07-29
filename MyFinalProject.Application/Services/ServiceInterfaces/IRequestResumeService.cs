using MyFinalProject.Application.DTOs;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
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

        Task ChangeStatusAsync(Guid requestId, RequestStatus newStatus);

        Task SendStatusChangedEmailAsync(RequestResume request, RequestStatus status);
    }
}
