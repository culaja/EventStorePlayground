using System;
using Common.Messaging;
using Domain.StudentDomain.Events;
using RabbitMqMessageBus.Mappings.Student;

namespace RabbitMqMessageBus.Mappings
{
    public static class DomainEventSerializer
    {
        public static byte[] Serialize(this IDomainEvent domainEvent) =>
            ResolveDomainEventDtoFrom(domainEvent).Serialize();

        private static DomainEventDto ResolveDomainEventDtoFrom(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case StudentCreated e: return new StudentCreatedDto(e);
                case StudentMoved e: return new StudentMovedDto(e);
                case StudentHired e: return new StudentHiredDto(e);
                default:
                    throw new NotSupportedException($"Domain event '{domainEvent}' is not supported to be serialized.");
            }
        }
    }
}