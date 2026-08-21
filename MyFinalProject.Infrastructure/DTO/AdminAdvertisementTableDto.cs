using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.DTO
{
    public class AdminAdvertisementTableDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EmployerName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
