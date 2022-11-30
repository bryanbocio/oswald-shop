using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    internal interface Specification<T>
    {

        Expression<Func<T, bool>> criteria { get; }
        List<Expression<Func<T, object>>> include { get; }
    }
}
