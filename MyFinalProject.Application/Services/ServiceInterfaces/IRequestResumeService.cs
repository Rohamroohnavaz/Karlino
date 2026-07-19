using MyFinalProject.Application.DTOs;
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
        Task ChangeRequestStatusAsync(ChangeRequestStatusDto dto);

        Task<List<RequestResume>> GetRequestsByAdverIdAsync(Guid advertisementId);

        Task<RequestResume?> GetRequestByUserId();

        Task<RequestResume?> GetRequestByAttachId(Guid attachId);
    }
}
