using System.Linq;
using Common;
using Common.Eventing;

namespace Ports.EventStore
{
    public static class EventApplier
    {
        public static int ApplyAllTo<T>(this IEventStore eventStore, IRepository<T> repository) where T : AggregateRoot => eventStore
            .LoadAllForAggregateStartingFrom<T>(0)
            .Select(domainEvent => HandleBasedOnType(domainEvent, repository))
            .Count();

        private static IDomainEvent HandleBasedOnType<T>(
            IDomainEvent domainEvent,
            IRepository<T> repository) where T : AggregateRoot
        {
            switch (domainEvent)
            {
                case AggregateRootCreated aggregateRootCreated:
                    repository.AddNew(
                        AggregateRoot.RestoreFrom<T>(aggregateRootCreated.AggregateRootCreationParameters));
                    break;
                default:
                    repository.Borrow(
                        domainEvent.AggregateRootId,
                        domainEvent.ApplyTo);
                    break;
            }

            return domainEvent;
        }
    }
}