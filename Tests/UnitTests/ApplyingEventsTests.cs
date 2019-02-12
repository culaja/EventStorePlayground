using Common;
using Domain;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
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
        private readonly IRepository<Student, StudentCreated> _studentRepository = new StudentInMemoryRepository(new NoOpMessageBus());

        [Fact]
        public void _1()
        {
            _eventStore.Append(new StudentCreated(
                StankoId,
                typeof(Student),
                StankoName,
                StankoEmailAddress,
                Maybe<City>.None,
                false));
            _eventStore.Append(StankoMovedToNoviSad);
            _eventStore.Append(StankoHired);

            _eventStore.ApplyAllTo(_studentRepository);

            var student = _studentRepository.BorrowBy(StankoId, s => s).Value;
            
            student.Id.Should().Be(StankoId);
            student.MaybeCity.Should().Be(NoviSad);
            student.IsHired.Should().BeTrue();
        }
    }
}