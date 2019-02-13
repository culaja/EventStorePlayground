using System;
using Common.Messaging;
using Domain.StudentDomain.Events;
using MongoDbEventStore.Mapping.StudentEvents;

namespace MongoDbEventStore.Mapping
{
    public static class DomainToDtoMapper
    {
        public static DomainEventDto ToDto(this IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case StudentCreated e:
                    return new StudentCreatedDto(e);
                case StudentMoved e:
                    return new StudentMovedDto(e);
                case StudentHired e:
                    return new StudentHiredDto(e);
                default:
                    throw new NotSupportedException($"Domain event type '{domainEvent.GetType()}' is not supported.");
            }
        }
    }
}