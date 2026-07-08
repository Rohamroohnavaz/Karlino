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
    public abstract class BaseModelBuilderConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CreatedAt);
            builder.HasQueryFilter(x => !x.IsDeleted);

            ApplyEntityConfiguration(builder);
        }

        protected abstract void ApplyEntityConfiguration(EntityTypeBuilder<TEntity> builder);
    }
}
