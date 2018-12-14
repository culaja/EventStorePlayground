using System;
using Common;

namespace Ports
{
    public interface IRepository<T> where T : AggregateRoot
    {   
        T Borrow(Guid aggregateRootId, Func<T, T> transformer);
    }
}