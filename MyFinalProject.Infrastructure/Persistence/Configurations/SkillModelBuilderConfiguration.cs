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
    public class SkillModelBuilderConfiguration : BaseModelBuilderConfiguration<Skill>
    {
        protected override void ApplyEntityConfiguration(EntityTypeBuilder<Skill> builder)
        {
            builder.Property(s => s.Name)
                .HasColumnType("NVARCHAR(120)")
                .IsRequired();

            builder.Property(s => s.Description)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired();

            builder.HasOne(s => s.SkillCategory)
                .WithMany(sc => sc.Skills)
                .HasForeignKey(s => s.SkillCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Skills)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
