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
    public class CategoryModelBuilderConfiguration : BaseModelBuilderConfiguration<Category>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.CategoryName)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnType("NVARCHAR(300)")
                .IsRequired();

            builder.HasIndex(c => c.CategoryName)
                .IsUnique();
        }
    }
}
