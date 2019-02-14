using System.Linq;
using System.Collections.Generic;
using Common.Messaging;
using MongoDbEventStore.Mapping;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Ports.EventStore;
using Shared.Common;

namespace MongoDbEventStore
{
    public sealed class EventStore : IEventStore
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMongoCollection<DomainEventDto> _mongoCollection;

        public EventStore(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _mongoCollection = databaseContext.GetCollectionFor<DomainEventDto>();
        }
        
        public IDomainEvent Append(IDomainEvent e)
        {
            _mongoCollection.InsertOne(e.ToDto());
            return e;
        }

        public IEnumerable<IDomainEvent> LoadAllFor<T>() where T : IAggregateEventSubscription, new()
        {
            var aggregateEventSubscription = new T();
            return _mongoCollection.AsQueryable()
                .Where(e => e.AggregateRootType == aggregateEventSubscription.AggregateTopicName).ToList()
                .Select(e => e.ToDomainEvent());
        }
    }
}