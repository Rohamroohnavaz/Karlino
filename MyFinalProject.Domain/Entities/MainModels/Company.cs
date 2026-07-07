using MyFinalProject.Domain.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Company : BaseEntity
    {
        public Company()
        {
            
        }

        public Company(string companyName ,string companyLocation)
        {
            CompanyName = companyName;
            CompanyLocation = companyLocation;
        }

        public string CompanyName { get; set; }
        public string CompanyLocation { get; set; }
        public User User { get; set; }
        public Attach Attach { get; set; }
        public List<Advertisement> Advertisements { get; set; } = new();

        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
                throw new Exception("CompanyName is null !!");

            if (string.IsNullOrWhiteSpace(CompanyLocation))
                throw new Exception("We don't have any location for company !!");
        }
    }
}
