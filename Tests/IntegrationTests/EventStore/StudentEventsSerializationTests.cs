using System.Linq;
using FluentAssertions;
using Xunit;
using static EventStore.DomainEventSerializer;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.EventStore
{
    public class StudentEventsSerializationTests
    {
        [Fact]
        public void Serializing_AggregateRootCreated() => 
            DeserializeToDomainEvent(Serialize(StankoCreated)).Should().Be(StankoCreated);

        [Fact]
        public void Serializing_StudentHired() =>
            DeserializeToDomainEvent(Serialize(StankoHired)).Should().Be(StankoHired);
        
        [Fact]
        public void Serializing_StudentMoved() =>
            DeserializeToDomainEvent(Serialize(StankoMovedToNoviSad)).Should().Be(StankoMovedToNoviSad);
    }
}