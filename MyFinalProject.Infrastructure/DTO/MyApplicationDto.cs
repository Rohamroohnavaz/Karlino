using MyFinalProject.Domain.Entities.Enums;
using System;

namespace MyFinalProject.Infrastructure.DTO
{
    public class MyApplicationDto
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public DateTime AppliedDate { get; set; }
        public RequestStatus Status { get; set; }
        public Guid? AdvertisementId { get; set; }
    }
}