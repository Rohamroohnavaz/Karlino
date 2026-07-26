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
    public class RevokedTokenModelBuilderConfiguration : IEntityTypeConfiguration<RevokedToken>
    {
        public void Configure(EntityTypeBuilder<RevokedToken> builder)
        {
            builder.HasKey(r => r.RevokeId);

            builder.Property(r => r.Jti)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();

            builder.Property(r => r.ExpiresAtUtc)
                .IsRequired();

            builder.Property(r => r.RevokedAtUtc)
                .IsRequired();

            builder.HasIndex(r => r.Jti)
                .IsUnique();

            builder.HasIndex(r => r.ExpiresAtUtc);
        }
    }
}
