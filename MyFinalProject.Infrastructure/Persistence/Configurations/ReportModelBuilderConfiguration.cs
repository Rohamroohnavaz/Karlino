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
    public class ReportModelBuilderConfiguration : BaseModelBuilderConfiguration<Report>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Reason)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired();

            builder.HasOne(r => r.Advertisement)
                .WithMany(a => a.Reports)
                .HasForeignKey(r => r.AdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Reporter)
               .WithMany()
               .HasForeignKey(r => r.ReporterId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Status)
               .HasConversion<string>()
               .IsRequired();
        }
    }
}
