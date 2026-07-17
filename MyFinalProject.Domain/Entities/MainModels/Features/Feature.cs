using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels.Features
{
    public class Feature : BaseEntity
    {
        private Feature()
        {

        }

        public Feature(string name ,decimal price)
        {
            Name = name;
            Price = price;
            Validation();
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public FeatureStatus Status { get; set; }
        public ICollection<CompanyFeature> CompanyFeatures { get; set; } = new List<CompanyFeature>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public DateTime StartDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
       
        public override void Validation()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidNameException("FeatureName is required !!");

            if (Price <= 0)
                throw new InvalidPriceException("FeaturePrice is Invalid !!");
        }
    }
}
