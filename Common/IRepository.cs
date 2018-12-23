using System;
using Common.Commanding;

namespace Common
{
    public interface IRepository<T> where T : AggregateRoot
    {
        T AddNew(T aggregateRoot);
        
        T Borrow(Guid aggregateRootId, Func<T, T> transformer);

        T BorrowEachFor(ISpecification<T> specification, Func<T, T> transformer);
    }
}