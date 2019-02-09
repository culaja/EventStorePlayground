using System;

namespace Common
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Result<T> AddNew(T aggregateRoot);
        
        Result<T> BorrowBy(Guid aggregateRootId, Func<T, T> transformer);
    }
}