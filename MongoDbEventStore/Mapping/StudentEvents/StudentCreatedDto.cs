using System;
using Common;
using Common.Messaging;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbEventStore.Mapping.StudentEvents
{
    public sealed class StudentCreatedDto : DomainEventDto
    {
        [BsonElement]
        public string Name { get; set; }
        
        [BsonElement]
        public string EmailAddress { get; set; }
        
        [BsonElement]
        public string City { get; set; }
        
        [BsonElement]
        public bool IsHired { get; set; }
        
        public StudentCreatedDto(StudentCreated e) : base(e)
        {
            Name = e.Name.ToString();
            EmailAddress = e.EmailAddress;
            City = e.MaybeCity.Map(c => c.ToString()).Unwrap();
            IsHired = e.IsHired;
        }

        protected override IDomainEvent ConvertDomainEvent() =>
            new StudentCreated(
                Guid.Parse(AggregateRootId),
                typeof(Student),
                new Name(Name),
                new EmailAddress(EmailAddress), 
                Maybe<string>.From(City).Map(c => new City(c)),
                IsHired);
    }
}