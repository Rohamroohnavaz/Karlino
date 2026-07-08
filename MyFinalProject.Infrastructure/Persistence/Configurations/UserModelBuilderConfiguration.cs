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
    public class UserModelBuilderConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                .HasColumnType("NVARCHAR(150)")
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnType("NVARCHAR(200)")
                .IsRequired();

            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.CreatedAt)
                .IsUnique();
            builder.HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
