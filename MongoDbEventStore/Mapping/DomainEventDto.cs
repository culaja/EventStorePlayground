using Common.Messaging;
using MongoDbEventStore.Mapping.StudentEvents;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbEventStore.Mapping
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(StudentCreatedDto),typeof(StudentMovedDto), typeof(StudentHiredDto))]
    public abstract class DomainEventDto
    {
        [BsonId]
        public BsonObjectId Id { get; set; }
        
        [BsonElement]
        public string AggregateRootId { get; set; }
        
        [BsonElement]
        public string AggregateRootType { get; set; }
        
        [BsonElement]
        public ulong Version { get; set; }

        protected DomainEventDto(IDomainEvent domainEvent)
        {
            Id = ObjectId.GenerateNewId();
            AggregateRootId = domainEvent.AggregateRootId.ToString();
            AggregateRootType = domainEvent.AggregateRootType.FullName;
            Version = domainEvent.Version;
        }

        public IDomainEvent ToDomainEvent() => ConvertDomainEvent().SetVersion(Version);
        
        protected abstract IDomainEvent ConvertDomainEvent();
    }
}