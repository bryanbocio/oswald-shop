using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    internal class BaseSpecification<T> : ISpecification<T>
    {
     
        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
            this.criteria = criteria;
        }
        
        
        public Expression<Func<T, bool>> criteria { get; }

        public List<Expression<Func<T, object>>> includes { get; }= new List<Expression<Func<T, object>>>();


        protected void AddInclude(Expression<Func<T,object>> includeExpression)
        {
            includes.Add(includeExpression);
        }


    }
}
