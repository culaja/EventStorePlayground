using Common;
using Common.Messaging;
using Ports.EventStore;

namespace CommonServices
{
    public sealed class AggregateRootCreatedPersistenceHandler : EventHandler<AggregateRootCreated>
    {
        private readonly IEventStore _eventStore;

        public AggregateRootCreatedPersistenceHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public override Result Handle(AggregateRootCreated e) => _eventStore
            .Append(e)
            .ToOkResult();
    }
}