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
    public class AttachModelBuilderConfiguration : BaseModelBuilderConfiguration<Attach>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Attach> builder)
        {
            builder.Property(a => a.FileName)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(a => a.FilePath)
                .HasColumnType("NVARCHAR(400)")
                .IsRequired();

            builder.Property(a => a.FileSize)
                .HasColumnType("BIGINT")
                .IsRequired();

            builder.Property(a => a.ContentType)
                .HasColumnType("NVARCHAR(200)")
                .IsRequired();

            builder.HasOne(a => a.Company)
                .WithMany()
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Advertisement)
                .WithMany()
                .HasForeignKey(a => a.AdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Advertisement)
                .WithMany()
                .HasForeignKey(a => a.AdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
