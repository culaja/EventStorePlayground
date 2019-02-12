using System;
using System.Collections.Generic;
using Common;
using Common.Messaging;
using Ports.EventStore;

namespace MongoDbEventStore
{
    public sealed class MongoDbEventStore : IEventStore
    {
        public IDomainEvent Append(IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDomainEvent> LoadAllFor<T>() where T : AggregateRoot
        {
            throw new NotImplementedException();
        }
    }
}