using System.Linq;
using Domain.StudentDomain;
using EventStore;
using FluentAssertions;
using Ports.EventStore;
using Xunit;
using static System.Guid;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.EventStore
{
    public sealed class EventStoreTests
    {
        private readonly IEventStore _eventStore = new EventStoreProvider(NewGuid().ToString());
        
        [Fact]
        public void _1()
        {
            _eventStore.Append(StankoMovedToNoviSad);

            var lastAddedEvent =  _eventStore.LoadAllFor<Student>().Last();

            lastAddedEvent.Should().Be(StankoMovedToNoviSad);
        }
    }
}