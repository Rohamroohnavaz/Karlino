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

        public Company(string companyName, string companyLocation)
        {
            CompanyName = companyName;
            CompanyLocation = companyLocation;
            Validation();
        }

        public string CompanyName { get; private set; }
        public string CompanyLocation { get; private set; }
        public User User { get; set; }
        public Guid UserId { get; private set; }
        public ICollection<Attach> Attaches { get; set; } = new List<Attach>();
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();

        public void ChangeCompanyName(string newCompanyName)
        {
            if (string.IsNullOrWhiteSpace(newCompanyName))
                throw new Exception("CompanName is required !");

            CompanyName = newCompanyName;
        }

        public void ChangeCompanyLocation(string newcompanyLocation)
        {
            if (string.IsNullOrWhiteSpace(newcompanyLocation))
                throw new Exception("CompanyLocation is required !");

            CompanyLocation = newcompanyLocation;
        }

        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(CompanyName))
                throw new Exception("CompanyName is null !!");

            if (string.IsNullOrWhiteSpace(CompanyLocation))
                throw new Exception("We don't have any location for company !!");
        }
    }
}
