using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.Enums;
using MyFinalProject.Domain.Entities.MainModels.Features;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class Payment : BaseEntity
    {
        private Payment()
        {

        }

        public Payment(decimal amount)
        {
            Amount = amount;
            Validation();
        }

        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Company? Company { get; set; }
        public Guid? CompanyId { get; set; }
        public Feature Feature { get; set; }
        public Guid? FeatureId { get; set; }

        public override void Validation()
        {
            if (Amount <= 0)
                throw new InvalidPriceException("Amount is invalid !!");

            if (CompanyId == Guid.Empty)
                throw new InvalidGuidException("CompanyId is required !!");

            if (FeatureId == Guid.Empty)
                throw new InvalidGuidException("FeatureId is required !!");
        }
    }
}
