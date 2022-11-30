using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }

        public async Task<T> getByIdAsync(int id)
        {
            var entity=await _storeContext.Set<T>().FindAsync(id);

            return entity;
        }

        public async Task<IReadOnlyList<T>> listAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<T> getEntityWithSpecification(ISpecification<T> specification)
        {
            return await applySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> listAsync(ISpecification<T> specification)
        {
            return await applySpecification(specification).ToListAsync();   
        }

        private IQueryable<T> applySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.getQuery(_storeContext.Set<T>().AsQueryable(), specification);
        }
       
    }
}
