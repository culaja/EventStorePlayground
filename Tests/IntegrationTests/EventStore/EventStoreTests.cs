using System.Linq;
using Domain;
using EventStore;
using FluentAssertions;
using Ports.EventStore;
using Xunit;
using static Tests.SomeStudentEvents;

namespace Tests.IntegrationTests.EventStore
{
    public sealed class EventStoreTests
    {
        private readonly IEventStore _eventStore = new EventStoreProvider();
        
        [Fact]
        public void _1()
        {
            _eventStore.Append(StankoMoved);

            var lastAddedEvent =  _eventStore.LoadAllStartingFrom<Student>().Last();

            lastAddedEvent.Should().Be(StankoMoved);
        }
    }
}