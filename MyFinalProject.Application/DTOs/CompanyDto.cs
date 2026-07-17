using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
    }
}
