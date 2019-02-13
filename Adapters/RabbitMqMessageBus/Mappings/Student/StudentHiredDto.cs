using Common.Messaging;
using Domain.StudentDomain.Events;

namespace RabbitMqMessageBus.Mappings.Student
{
    public sealed class StudentHiredDto : DomainEventDto
    {   
        public StudentHiredDto()
        {
        }

        public StudentHiredDto(StudentHired e) : base(e)
        {
        }

        protected override IDomainEvent ToDomainEventInternal() =>
            new StudentHired(AggregateRootId);
    }
}