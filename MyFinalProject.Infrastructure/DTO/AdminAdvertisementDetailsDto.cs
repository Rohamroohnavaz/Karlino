using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.DTO
{
    public class AdminAdvertisementDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string EmployerName { get; set; } = string.Empty;
        public string EmployerEmail { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string CategoryTitle { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
