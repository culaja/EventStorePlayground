using System;
using Aggregate.Student.Shared;
using Common;
using Common.Messaging;
using Domain.StudentDomain.Events;
using Shared.Common;

namespace RabbitMqMessageBus.Serialization
{
    public static class DomainEventSerializer
    {
        public static byte[] Serialize(this IDomainEvent domainEvent) =>
            ResolveSharedEventFrom(domainEvent).Serialize();

        private static SharedEvent ResolveSharedEventFrom(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case StudentCreated e:
                    return new StudentCreatedShared
                    {
                        AggregateRootId = e.AggregateRootId,
                        Version = e.Version,
                        Name = e.Name.ToString(),
                        EmailAddress = e.EmailAddress,
                        City = e.MaybeCity.Map(c => c.ToString()).Unwrap(),
                        IsHired = e.IsHired
                    };
                case StudentMoved e:
                    return new StudentMovedShared
                    {
                        AggregateRootId = e.AggregateRootId,
                        Version = e.Version,
                        City = e.City.ToString()
                    };
                case StudentHired e:
                    return new StudentHiredShared
                    {
                        AggregateRootId = e.AggregateRootId,
                        Version = e.Version
                    };
                default:
                    throw new NotSupportedException($"Domain event '{domainEvent}' is not supported to be serialized.");
            }
        }
    }
}