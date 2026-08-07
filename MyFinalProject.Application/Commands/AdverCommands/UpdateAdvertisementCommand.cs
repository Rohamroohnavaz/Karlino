using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Application.Commands.AdverCommands
{
    public class UpdateAdvertisementCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public string CompanyName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public Guid CompanyId { get; set; }
    }
}
