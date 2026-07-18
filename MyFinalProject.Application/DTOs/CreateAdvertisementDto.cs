using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.DTOs
{
    public class CreateAdvertisementDto
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Salary { get; private set; }
        public string CompanyName { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
    }
}
