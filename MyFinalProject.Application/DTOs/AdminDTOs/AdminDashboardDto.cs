using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs.AdminDTOs
{
    public class AdminDashboardDto
    {
        public int TotalJobSeekers { get; set; }
        public int TotalEmployers { get; set; }
        public int ActiveJobPostings { get; set; }
        public int InactiveJobPostings { get; set; }
        public int PendingEmployersCount { get; set; }
        public Dictionary<string, int> RequestResumeStats { get; set; } = new();
    }
}
