using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinalProject.Domain.Entities.MainModels.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Persistence.Configurations
{
    public class FeatureModelBuilderConfiguration : BaseModelBuilderConfiguration<Feature>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(f => f.Name)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(f => f.Price)
                .IsRequired();


        }
    }
}
