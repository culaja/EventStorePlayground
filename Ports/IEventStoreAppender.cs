using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;

namespace Ports
{
    public interface IEventStoreAppender
    {
        Task<IReadOnlyList<IDomainEvent>> AsyncLoadAllEventsFor<T>(AggregateId aggregateId) where T : AggregateRoot, new();

        Task<Nothing> AppendAsync<T>(
            AggregateId aggregateId, 
            IReadOnlyList<IDomainEvent> domainEvents,
            long expectedVersion)  where T : AggregateRoot, new();
    }
}