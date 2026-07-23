using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Filters
{
    public class AdverSearchFilterDto
    {
        public string? SearchTerm { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? CityId { get; set; }
        public bool? IsActive { get; set; }
        public decimal? MinSalary { get; set; }
    }
}
