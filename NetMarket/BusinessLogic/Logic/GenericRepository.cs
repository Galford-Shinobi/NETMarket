using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClaseBase
    {
        private readonly MarketDbContext _marketDbContext;

        public GenericRepository(MarketDbContext marketDbContext)
        {
            _marketDbContext = marketDbContext;
        }

        public async Task<int> AddAsync(T entity)
        {
            _marketDbContext.Set<T>().Add(entity);
            return await _marketDbContext.SaveChangesAsync();
        }

        public void AddEntity(T Entity)
        {
            _marketDbContext.Set<T>().Add(Entity);
        }

        public void DeleteEntity(T Entity)
        {
            _marketDbContext.Set<T>().Remove(Entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _marketDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _marketDbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _marketDbContext.Set<T>().Attach(entity);
            _marketDbContext.Entry(entity).State = EntityState.Modified;
            return await _marketDbContext.SaveChangesAsync();
        }

        public void UpdateEntity(T Entity)
        {
            _marketDbContext.Set<T>().Attach(Entity);
            _marketDbContext.Entry(Entity).State = EntityState.Modified;
        }
        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_marketDbContext.Set<T>().AsQueryable(), spec);
        }

    }
}
