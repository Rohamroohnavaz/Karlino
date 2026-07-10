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
        
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Salary { get; private set; }
        public string CompanyName { get; private set; }
        public RequestResume RequestResume { get; private set; }
        public Guid? RequestResumeId { get; private set; }
        public Company Company { get; private set; }
        public Guid CompanyId { get; private set; }
        public User User { get; private set; }
        public ICollection<Attach> Attaches { get; set; } = new List<Attach>();

        public void ChangeTitle(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
                throw new Exception("Title is required !!");

            Title = newTitle;
        }

        public void ChangeDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
                throw new Exception("Description is required !!");

            Description = newDescription;
        }

        public void ChangeCompanyName(string newCompanyName)
        {
            if (string.IsNullOrWhiteSpace(newCompanyName))
                throw new Exception("CompanyName is required !!");

            CompanyName = newCompanyName;
        }

        public void ChangeSalary(decimal newSalary)
        {
            if(newSalary <= 0)
                throw new Exception("Salary is invalid !!");

            Salary = newSalary;
        }

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
