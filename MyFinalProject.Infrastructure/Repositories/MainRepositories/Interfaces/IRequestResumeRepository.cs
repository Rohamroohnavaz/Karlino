using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IRequestResumeRepository : IGenericRepository<RequestResume>
    {
        Task<List<RequestResume>> GetRequestByAdverId(Guid adverId);

        Task<RequestResume?> GetRequestWithAdvertisement(Guid requestId);

        Task<RequestResume?> GetRequestWithAttach(Guid requestId);

        Task<RequestResume?> GetRequestResumeByCompanyId(Guid resumeId, Guid companyId);

        Task<RequestResume?> GetRequestResumeWithAttachByCompanyId(Guid resumeId, Guid companyId);

        Task<RequestResume?> GetResumeByAttachId(Guid attachId);

        Task<RequestResume?> GetRequestByUserId(Guid userId);

        Task<RequestResume?> GetRequestWithAttachmentByUserId(Guid requestId, Guid userId);

        Task<bool> ExistsByUserAndAdvertisement(Guid userId, Guid advertisementId);

        Task<Dictionary<string, int>> GetStatusStats();

        Task<List<MyApplicationDto>> GetMyApplicationsAsync(Guid userId);
    }
}
