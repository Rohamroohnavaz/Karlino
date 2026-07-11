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
    public class PaymentModelBuilderConfiguring : BaseModelBuilderConfiguration<Payment>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Amount)
                .IsRequired();

            builder.HasOne(p => p.Company)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Feature)
                .WithMany(f => f.Payments)
                .HasForeignKey(p => p.FeatureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
