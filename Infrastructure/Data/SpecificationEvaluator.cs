using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> getQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spectification)
        {
            var query = inputQuery;

            if (spectification.criteria != null)
            {
                query = query.Where(spectification.criteria);
            }

            query = spectification.includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
