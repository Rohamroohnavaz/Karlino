using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Domain.Entities.MainModels.Features;
using MyFinalProject.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Domain.Entities.MainModels
{
    public class CompanyFeature : BaseEntity
    {
        public Feature? Feature { get; private set; }
        public Guid? FeatureId { get; private set; }
        public Company? Company { get; private set; }
        public Guid? CompanyId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsActive => DateTime.UtcNow == EndTime;

        public override void Validation()
        {
            if (Feature == null && FeatureId == Guid.Empty)
                throw new InvalidFeatureFieldsException("FeatureFields are invalid !!");

            if (CompanyId == Guid.Empty)
                throw new InvalidGuidException($"{nameof(CompanyId)} is invalid !!");

            if (Company == null)
                throw new InvalidNameException("Company is required !!");
        }
    }
}
