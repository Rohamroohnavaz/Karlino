using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs.AdminDTOs
{
    public class AdminEmployerTableDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime RegisteredAt { get; set; }
    }
}
