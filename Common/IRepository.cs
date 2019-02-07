using System;
using Common.Commanding;

namespace Common
{
    public interface IRepository<T> where T : AggregateRoot
    {
        T AddNew(T aggregateRoot);
        
        T Borrow(Guid aggregateRootId, Func<T, T> transformer);
    }
}