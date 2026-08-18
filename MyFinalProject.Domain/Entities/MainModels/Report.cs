using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Report : BaseEntity
    {
        protected Report() { }

        public Report(Guid advertisementId, Guid reporterId, string reason)
        {
            AdvertisementId = advertisementId;
            ReporterId = reporterId;
            Reason = reason;
            CreatedAt = DateTime.Now;
            Status = ReportStatus.Pending;
        }

        public DateTime CreatedAt { get; set; }
        public Guid AdvertisementId { get; private set; }
        public Guid ReporterId { get; private set; }
        public string Reason { get; private set; } = string.Empty;
        public ReportStatus Status { get; private set; }

        public Advertisement Advertisement { get; private set; } = null!;
        public User Reporter { get; private set; } = null!;

        public void ChangeStatus(ReportStatus status)
        {
            Status = status;
        }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Reason))
                throw new Exception("Invalid Reason !");
        }
    }
}
