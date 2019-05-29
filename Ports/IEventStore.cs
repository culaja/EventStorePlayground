using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;

namespace Ports
{
    public interface IEventStore
    {
        Task<IReadOnlyList<IDomainEvent>> LoadAllEventsForAsync(AggregateId aggregateId);

        Task<Nothing> AppendAsync(
            AggregateId aggregateId, 
            long expectedVersion, 
            IReadOnlyList<IDomainEvent> domainEvents);
    }
}