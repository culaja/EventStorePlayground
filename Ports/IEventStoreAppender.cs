using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;

namespace Ports
{
    public interface IEventStoreAppender
    {
        Task<IReadOnlyList<IDomainEvent>> AsyncLoadAllEventsFor(AggregateId aggregateId);

        Task<Nothing> AppendAsync(
            AggregateId aggregateId, 
            IReadOnlyList<IDomainEvent> domainEvents,
            long expectedVersion);
    }
}