using Common.Messaging;
using Domain.StudentDomain.Events;
using static Domain.City;

namespace RabbitMqMessageBus.Mappings.Student
{
    public sealed class StudentMovedDto : DomainEventDto
    {
        public string City { get; set; }
        
        public StudentMovedDto()
        {
        }

        public StudentMovedDto(StudentMoved e) : base(e)
        {
            City = e.City.ToString();
        }

        protected override IDomainEvent ToDomainEventInternal() =>
            new StudentMoved(AggregateRootId, CityFrom(City));
    }
}