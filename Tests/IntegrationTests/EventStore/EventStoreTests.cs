using System;
using System.Linq;
using Autofac.Core;
using Domain.StudentDomain;
using FluentAssertions;
using Mongo2Go;
using MongoDbEventStore;
using Ports.EventStore;
using Xunit;
using static System.Guid;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.EventStore
{
    public sealed class EventStoreTests : IDisposable
    {
        private readonly MongoDbRunner _runner = MongoDbRunner.Start();
        private readonly IEventStore _eventStore;

        public EventStoreTests()
        {
            var connectionString = _runner.ConnectionString;
            _eventStore = new MongoDbEventStore.EventStore(new DatabaseContext(connectionString, NewGuid().ToString()));
        }

        public void Dispose()
        {
            _runner.Dispose();
        }

        [Fact]
        public void _1()
        {
            _eventStore.Append(StankoCreated);
            _eventStore.Append(StankoMovedToNoviSad);
            _eventStore.Append(StankoHired);

            var allEvents =  _eventStore.LoadAllFor<Student>().ToList();

            allEvents.Should().BeEquivalentTo(
                StankoCreated,
                StankoMovedToNoviSad,
                StankoHired);
        }
    }
}