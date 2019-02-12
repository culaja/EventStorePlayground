using System;
using Common.Messaging;
using Domain;
using Domain.StudentDomain.Events;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbEventStore.Mapping.StudentEvents
{
    public sealed class StudentMovedDto : DomainEventDto
    {
        [BsonElement]
        public string City { get; set; }
        
        public StudentMovedDto(StudentMoved e) : base(e)
        {
            City = e.City.ToString();
        }

        public override IDomainEvent ToDomainEvent()
            => new StudentMoved(
                Guid.Parse(AggregateRootId),
                Version,
                new City(City));
    }
}