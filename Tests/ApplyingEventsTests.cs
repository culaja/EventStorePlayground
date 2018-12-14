using Common;
using Domain;
using FluentAssertions;
using InMemory;
using Ports;
using Xunit;
using static System.Guid;
using static Domain.City;

namespace Tests
{
    public sealed class ApplyingEventsTests
    {
        private readonly IEventStore _eventStore = new InMemoryEventStore();
        private readonly IRepository<Student> _studentRepository = new InMemoryRepository<Student>();

        [Fact]
        public void _1()
        {
            var studentId = NewGuid();
            _eventStore.Append(new AggregateRootCreated(studentId, typeof(Student)));
            _eventStore.Append(new StudentMoved(studentId, Belgrade));
            _eventStore.Append(new StudentHired(studentId));

            _eventStore.ApplyAllTo(_studentRepository);

            var student = _studentRepository.Borrow(studentId, s => s);
            
            student.Id.Should().Be(studentId);
            student.MaybeCity.Should().Be(Belgrade);
            student.IsHired.Should().BeTrue();
        }
    }
}