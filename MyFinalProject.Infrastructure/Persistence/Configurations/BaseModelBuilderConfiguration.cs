using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyFinalProject.Domain.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Persistence.Configurations
{
    public abstract class BaseModelBuilderConfiguration<TEntity>
        : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CreatedAt);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Creater)
                .WithMany()
                .HasForeignKey(x => x.CreateById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Modifier)
                .WithMany()
                .HasForeignKey(x => x.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Deleter)
                .WithMany()
                .HasForeignKey(x => x.DeletedById)
                .OnDelete(DeleteBehavior.Restrict);

            ApplyEntityConfiguration(builder);
        }

        protected abstract void ApplyEntityConfiguration(EntityTypeBuilder<TEntity> builder);
    }
}
