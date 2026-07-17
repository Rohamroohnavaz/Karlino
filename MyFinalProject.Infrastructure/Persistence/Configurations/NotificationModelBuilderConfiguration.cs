using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinalProject.Domain.Entities.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Persistence.Configurations
{
    public class NotificationModelBuilderConfiguration : BaseModelBuilderConfiguration<Notification>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(n => n.Title)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(n => n.Message)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.Company)
                .WithMany(c => c.Notifications)
                .HasForeignKey(n => n.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
