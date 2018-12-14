using System.Linq;
using Common;
using Ports.EventStore;

namespace Ports
{
    public static class EventApplier
    {
        public static int ApplyAllTo<T>(this IEventStore eventSource, IRepository<T> eventDestination) where T : AggregateRoot => eventSource
            .LoadAllStartingFrom<T>(0)
            .Select(domainEvent => eventDestination
                .Borrow(domainEvent.AggregateRootId, domainEvent.ApplyTo))
            .Count();
    }
}