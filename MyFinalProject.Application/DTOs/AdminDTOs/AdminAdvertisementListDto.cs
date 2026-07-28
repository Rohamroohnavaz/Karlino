using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs.AdminDTOs
{
    public class AdminAdvertisementListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime? FeaturedUntil { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
