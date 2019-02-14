using Aggregate.Student.Shared;
using Common;
using Domain.StudentDomain;
using FluentAssertions;
using InMemory;
using Ports.EventStore;
using Tests.IntegrationTests;
using Xunit;
using static Domain.City;
using static Tests.StudentsValues;

namespace Tests.UnitTests
{
    public sealed class ApplyingEventsTests
    {
        private readonly IEventStore _eventStore = new InMemoryEventStore();
        private readonly IRepository<Student, StudentCreated> _studentRepository = new StudentInMemoryRepository(new NoOpLocalMessageBus());

        [Fact]
        public void _1()
        {
            _eventStore.Append(StankoCreated.SetVersion(1));
            _eventStore.Append(StankoMovedToNoviSad.SetVersion(2));
            _eventStore.Append(StankoHired.SetVersion(3));

            _eventStore.ApplyAllTo<Student, StudentCreated, StudentEventSubscription>(_studentRepository);

            var student = _studentRepository.BorrowBy(StankoId, s => s).Value;
            
            student.Id.Should().Be(StankoId);
            student.MaybeCity.Should().Be(NoviSad);
            student.IsHired.Should().BeTrue();
        }
    }
}