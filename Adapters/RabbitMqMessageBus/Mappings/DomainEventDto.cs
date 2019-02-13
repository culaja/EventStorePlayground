using System;
using Common.Messaging;

namespace RabbitMqMessageBus.Mappings
{
    public abstract class DomainEventDto
    {
        public Guid AggregateRootId { get; set; }
        public ulong Version { get; set; }

        protected DomainEventDto()
        {
        }
        
        protected DomainEventDto(IDomainEvent domainEvent)
        {
            AggregateRootId = domainEvent.AggregateRootId;
            Version = domainEvent.Version;
        }

        public IDomainEvent ToDomainEvent() =>
            ToDomainEventInternal().SetVersion(Version);

        protected abstract IDomainEvent ToDomainEventInternal();

    }
}