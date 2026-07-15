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
    public class AdvertisementModelBuilderConfiguring : BaseModelBuilderConfiguration<Advertisement>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(a => a.Title)
                .HasColumnType("NVARCHAR(200)")
                .IsRequired();

            builder.Property(a => a.Description)
                .HasColumnType("NVARCHAR(350)")
                .IsRequired();

            builder.Property(a => a.Salary)
                .HasColumnType("DECIMAL")
                .IsRequired();

            builder.Property(a => a.CompanyName)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(a => a.StartDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(a => a.ExpireDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.HasOne(a => a.Company)
                .WithMany(c => c.Advertisements)
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Category)
                .WithMany(c => c.Advertisements)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
