using System;

namespace Common
{
    public interface IRepository<T, Tk>
        where T : AggregateRoot
        where Tk: AggregateRootCreated
    {
        Result<T> CreateFrom(Tk aggregateRootCreated);

        Result<T> AddNew(T aggregateRoot);
        
        Result<T> BorrowBy(Guid aggregateRootId, Func<T, T> transformer);
    }
}