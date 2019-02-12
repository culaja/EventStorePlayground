using System.Linq;
using Common;
using Common.Messaging;

namespace Ports.EventStore
{
    public static class EventApplier
    {
        public static int ApplyAllTo<T, Tk>(this IEventStore eventStore, IRepository<T, Tk> repository) 
            where T : AggregateRoot 
            where Tk: AggregateRootCreated => eventStore
            .LoadAllForAggregateStartingFrom<T>(0)
            .Select(domainEvent => HandleBasedOnType(domainEvent, repository))
            .Count();

        private static IDomainEvent HandleBasedOnType<T, Tk>(
            IDomainEvent domainEvent,
            IRepository<T, Tk> repository) 
            where T : AggregateRoot
            where Tk: AggregateRootCreated
        {
            switch (domainEvent)
            {
                case Tk aggregateRootCreated:
                    repository.CreateFrom(aggregateRootCreated);
                    break;
                default:
                    repository.BorrowBy(
                        domainEvent.AggregateRootId,
                        domainEvent.ApplyTo);
                    break;
            }

            return domainEvent;
        }
    }
}