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
    public class RequestResumeModelBuilderConfiguration : BaseModelBuilderConfiguration<RequestResume>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<RequestResume> builder)
        {
            builder.Property(r => r.JobSeekerName)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(r => r.JobSeekerLastName)
                .HasColumnType("NVARCHAR(200)")
                .IsRequired();

            builder.Property(r => r.StartDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(r => r.ExpireDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.RequestResumes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Advertisement)
               .WithMany(a => a.RequestResumes)
               .HasForeignKey(r => r.AdvertisementId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Attach)
                .WithOne()
                .HasForeignKey<RequestResume>(r => r.AttachmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(r => new { r.UserId, r.AdvertisementId })
                   .IsUnique()
                   .HasFilter("[IsDeleted] = 0");
        }
    }
}
