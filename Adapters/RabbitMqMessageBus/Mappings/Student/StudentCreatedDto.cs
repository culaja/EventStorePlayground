using Aggregate.Student.Shared;
using Common;
using Common.Messaging;
using Domain.StudentDomain.Events;
using static Common.Maybe<string>;
using static Domain.Name;
using static Domain.EmailAddress;
using static Domain.City;

namespace RabbitMqMessageBus.Mappings.Student
{
    public sealed class StudentCreatedDto : DomainEventDto, IStudentCreatedDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string City { get; set; }
        public bool IsHired { get; set; }

        public StudentCreatedDto()
        {
        }
        
        public StudentCreatedDto(StudentCreated e) : base(e)
        {
            Name = e.Name.ToString();
            EmailAddress = e.EmailAddress;
            City = e.MaybeCity.Map(c => c.ToString()).Unwrap();
            IsHired = e.IsHired;
        }

        protected override IDomainEvent ToDomainEventInternal() =>
            new StudentCreated(
                AggregateRootId,
                NameFrom(Name), 
                EmailAddressFrom(EmailAddress),
                From(City).Map(s => CityFrom(s)),
                IsHired);
    }
}