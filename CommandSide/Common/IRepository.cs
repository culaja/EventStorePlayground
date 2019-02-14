using System;
using Shared.Common;

namespace Common
{
    public interface IRepository<T, in TK>
        where T : AggregateRoot
        where TK: IAggregateRootCreated
    {
        Result<T> CreateFrom(TK aggregateRootCreated);

        Result<T> AddNew(T aggregateRoot);
        
        Result<T> BorrowBy(Guid aggregateRootId, Func<T, T> transformer);
    }
}