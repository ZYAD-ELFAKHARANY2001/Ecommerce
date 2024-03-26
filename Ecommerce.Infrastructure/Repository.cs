using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure
{
    public class Repository<TEntity, Tid> : IRepository<TEntity, Tid> where TEntity : class
    {
        private readonly EcommerceContext _ecommerceContext;
        private readonly DbSet<TEntity> _Dbset;
        public Repository(EcommerceContext ecommerceContext)
        {
            _ecommerceContext = ecommerceContext;
            _Dbset = _ecommerceContext.Set<TEntity>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _Dbset.AddAsync(entity)).Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {

            return Task.FromResult((_Dbset.Remove(entity)).Entity);
        }

        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_Dbset.Select(s=>s));
        }

        public async Task<TEntity> GetByIdAsync(Tid id)
        {
            return await _Dbset.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
          return await _ecommerceContext.SaveChangesAsync();
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult( _Dbset.Update(entity).Entity);

        }
    }
}
