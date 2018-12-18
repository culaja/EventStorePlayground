using System;
using System.Linq.Expressions;

namespace Common.Commanding
{
    public interface ISpecification<T> where T : AggregateRoot
    {
        Expression<Func<T, bool>> IsSatisfied();
    }
}