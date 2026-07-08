using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Advertisement : BaseEntity
    {
        public Advertisement()
        {
            
        }

        public Advertisement(string title ,string description ,decimal salary)
        {
            Title = title;
            Description = description;
            Salary = salary;
            Validation();
        }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public string CompanyName { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public User User { get; set; }
        public ICollection<Attach> Attaches { get; set; } = new List<Attach>();

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new Exception();

            if (string.IsNullOrWhiteSpace(Description))
                throw new Exception();

            if(Salary <= 0)
                throw new Exception();

            if (string.IsNullOrWhiteSpace(CompanyName))
                throw new Exception();
        }
    }
}
