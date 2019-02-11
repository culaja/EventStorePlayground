using Common;
using Common.Messaging;
using Ports.EventStore;
using IDomainEvent = Common.Messaging.IDomainEvent;

namespace CommonServices
{
    public sealed class DomainEventPersistenceHandler : EventHandler<IDomainEvent>
    {
        private readonly IEventStore _eventStore;

        public DomainEventPersistenceHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override Result Handle(IDomainEvent e) => _eventStore
            .Append(e)
            .ToOkResult();
    }
}