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
    public class SettingConfiguration : BaseModelBuilderConfiguration<Setting>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(s => s.Key)
                .IsUnique();

            builder.Property(s => s.Value)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
