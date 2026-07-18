using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.LogManager;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Company : BaseEntity
    {
        private Company()
        {

        }

        public Company(string companyName, string companyLocation, string province, string city)
        {
            CompanyName = companyName;
            CompanyLocation = companyLocation;
            Province = province;
            City = city;
            Validation();
        }

        public string CompanyName { get; private set; }
        public string CompanyLocation { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public User? User { get; set; }
        public Guid? UserId { get; private set; }
        public ICollection<Attach> Attaches { get; set; } = new List<Attach>();
        public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<CompanyFeature> CompanyFeatures { get; set; } = new List<CompanyFeature>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

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
                throw new InvalidTextException("CompanyName is null !!");

            if (string.IsNullOrWhiteSpace(CompanyLocation))
                throw new InvalidTextException("We don't have any location for company !!");

            if (string.IsNullOrWhiteSpace(Province))
                throw new InvalidTextException("We don't have any province for company !!");

            if (string.IsNullOrWhiteSpace(City))
                throw new InvalidTextException("We don't have any city for company !!");
        }
    }
}
