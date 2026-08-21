using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.DTO;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class ReportRepository : IReportRepository
    {
        private readonly FinalDbContext _dbContext;

        public ReportRepository(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AdminReportTableDto>> GetAllAsync()
        {
            return await _dbContext.Reports
                .OrderByDescending(r => r.Status == ReportStatus.Pending)
                .ThenByDescending(r => r.CreatedAt)
                .Select(r => new AdminReportTableDto
                {
                    Id = r.Id,
                    AdvertisementId = r.AdvertisementId,
                    AdvertisementTitle = r.Advertisement.Title,
                    ReporterEmail = r.Reporter.Email,
                    Reason = r.Reason,
                    CreatedAt = r.CreatedAt,
                    Status = r.Status
                }).ToListAsync();   
        }

        public async Task<bool> ChangeStatusAsync(Guid id, ReportStatus status)
        {
            var report = await _dbContext.Reports.FindAsync(id);

            if (report == null)
                return false;

            report.ChangeStatus(status);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Guid advertisementId, Guid reporterId, string reason)
        {
            _dbContext.Reports.Add(new Report(advertisementId, reporterId, reason));
            await _dbContext.SaveChangesAsync();
        }
    }
}
