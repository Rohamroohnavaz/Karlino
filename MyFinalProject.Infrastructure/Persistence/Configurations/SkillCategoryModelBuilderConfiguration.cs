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
    public class SkillCategoryModelBuilderConfiguration : BaseModelBuilderConfiguration<SkillCategory>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<SkillCategory> builder)
        {
            builder.Property(sc => sc.Name)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            builder.Property(sc => sc.Description)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
        }
    }
}
