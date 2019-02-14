using System;
using Aggregate.Student.Shared;
using Common.Messaging;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbEventStore.Mapping.StudentEvents
{
    public sealed class StudentHiredDto : DomainEventDto
    {
        [BsonElement]
        public string City { get; set; }
        
        public StudentHiredDto(StudentHired e) : base(e)
        {
        }

        protected override IDomainEvent ConvertDomainEvent()
            => new StudentHired(
                Guid.Parse(AggregateRootId));
    }
}