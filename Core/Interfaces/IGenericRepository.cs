using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> getByIdAsync(int id);
        Task<IReadOnlyList<T>> listAllAsync();

        Task<T> getEntityWithSpecification(ISpecification<T> specification);

        Task<IReadOnlyList<T>> listAsync (ISpecification<T> specification); 
    }
}
