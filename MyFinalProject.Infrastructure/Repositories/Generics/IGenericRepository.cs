using MyFinalProject.Domain.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.Generics
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);

        Task<T?> GetByIdAsync(Guid id ,bool tracking = true);

        Task<List<T>> QueryAsync(Expression<Func<T ,bool>> predicate ,bool tracking = false);

        Task UpdateAsync(T entity);

        Task SoftDeleteAsync(Guid id);

        Task HardDeleteAsync(Guid id);
    }
}
