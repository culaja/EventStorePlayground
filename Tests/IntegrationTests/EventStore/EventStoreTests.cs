using System.Linq;
using Common;
using Domain;
using Domain.StudentDomain;
using EventStore;
using FluentAssertions;
using Ports.EventStore;
using Xunit;
using static System.Guid;
using static Tests.SomeStudentEvents;

namespace Tests.IntegrationTests.EventStore
{
    public sealed class EventStoreTests
    {
        private readonly IEventStore _eventStore = new EventStoreProvider(NewGuid().ToString());
        
        [Fact]
        public void _1()
        {
            _eventStore.Append(StankoMoved);

            var lastAddedEvent =  _eventStore.LoadAllForAggregateStartingFrom<Student>().Last();

            lastAddedEvent.Should().Be(StankoMoved);
        }
    }
}