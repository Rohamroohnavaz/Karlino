using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.DTO
{
    public class AdminReportTableDto
    {
        public Guid Id { get; set; }
        public Guid AdvertisementId { get; set; }
        public string AdvertisementTitle { get; set; } = string.Empty;
        public string ReporterEmail { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ReportStatus Status { get; set; }
    }
}
