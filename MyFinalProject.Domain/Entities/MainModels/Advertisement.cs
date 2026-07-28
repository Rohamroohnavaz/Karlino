using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Advertisement : BaseEntity
    {
        private Advertisement() { }

        public Advertisement(string title, string description, decimal salary
            , string province, string city, string companyName, Guid companyId, Guid categoryId)
        {
            Title = title;
            Description = description;
            Salary = salary;
            Province = province;
            City = city;
            CompanyName = companyName;
            CompanyId = companyId;
            CategoryId = categoryId;
            //StartDate = startDate;
            //ExpireDate = expireDate;
            Validation();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Salary { get; private set; }
        public string CompanyName { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public ICollection<RequestResume> RequestResumes { get; set; } = new List<RequestResume>();
        public Company? Company { get; private set; }
        public Guid? CompanyId { get; private set; }
        public User? User { get; private set; }
        public ICollection<Attach> Attaches { get; set; } = new List<Attach>();
        public Category? Category { get; private set; }
        public Guid? CategoryId { get; private set; }
        public bool IsActive { get; set; } = true;
        public bool IsFeatured { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? FeaturedUntil { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }

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
            if (newSalary <= 0)
                throw new Exception("Salary is invalid !!");

            Salary = newSalary;
        }

        public void ChangeCity(string newCity)
        {
            if (string.IsNullOrWhiteSpace(newCity))
                throw new Exception("CityName is required !!");

            City = newCity;
        }

        public void ChangeProvince(string newProvince)
        {
            if (string.IsNullOrWhiteSpace(newProvince))
                throw new Exception("ProvinceName is required !!");

            Province = newProvince;
        }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Title))
                throw new Exception();

            if (string.IsNullOrWhiteSpace(Description))
                throw new Exception();

            if (Salary <= 0)
                throw new Exception();

            if (string.IsNullOrWhiteSpace(CompanyName))
                throw new Exception();
        }
    }
}
