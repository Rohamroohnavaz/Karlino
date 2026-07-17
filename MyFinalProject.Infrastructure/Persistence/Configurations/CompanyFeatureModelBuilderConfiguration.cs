using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinalProject.Domain.Entities.MainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Persistence.Configurations
{
    internal class CompanyFeatureModelBuilderConfiguration : BaseModelBuilderConfiguration<CompanyFeature>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<CompanyFeature> builder)
        {
            builder.HasOne(cf => cf.Company)
                .WithMany(c => c.CompanyFeatures)
                .HasForeignKey(cf => cf.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cf => cf.Feature)
                .WithMany(f => f.CompanyFeatures)
                .HasForeignKey(cf => cf.FeatureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
