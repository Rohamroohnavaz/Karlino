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
    public class CompanyModelBuilderConfiguration : BaseModelBuilderConfiguration<Company>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.CompanyName)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(c => c.CompanyLocation)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();           
        }
    }
}
