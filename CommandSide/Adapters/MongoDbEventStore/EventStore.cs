using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Messaging;
using MongoDbEventStore.Mapping;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Ports.EventStore;

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

        public IEnumerable<IDomainEvent> LoadAllFor<T>() where T : AggregateRoot => _mongoCollection.AsQueryable()
            .Where(e => e.AggregateRootType == typeof(T).ToString()).ToList()
            .Select(e => e.ToDomainEvent());
    }
}