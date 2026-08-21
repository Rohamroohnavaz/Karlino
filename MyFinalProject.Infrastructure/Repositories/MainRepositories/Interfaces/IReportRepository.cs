using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IReportRepository
    {
        Task<List<AdminReportTableDto>> GetAllAsync();
        Task<bool> ChangeStatusAsync(Guid id, ReportStatus status);
        Task AddAsync(Guid advertisementId, Guid reporterId, string reason);
    }
}