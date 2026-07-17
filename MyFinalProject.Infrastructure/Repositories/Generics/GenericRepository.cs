using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.Abstraction;
using MyFinalProject.Infrastructure.Persistence.UnitOfWorkFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.Generics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IBaseEntity
    {
        protected readonly FinalDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        protected GenericRepository(FinalDbContext dbContext, IUnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public GenericRepository(FinalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> predicate
            , bool tracking = false)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            return await query
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, bool tracking = false)
        {
            var query = _dbContext.Set<T>().AsQueryable().Where(q => q.IsDeleted == false);

            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task HardDeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
                return;

            _dbContext.Set<T>().Remove(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
                return;

            entity.SetDeleted(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
