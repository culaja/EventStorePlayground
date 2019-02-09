using FluentAssertions;
using Xunit;
using static EventStore.DomainEventSerializer;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.EventStore
{
    public class EventSerializationTests
    {
        [Fact]
        public void Serializing_StudentHired() =>
            DeserializeToDomainEvent(Serialize(StankoHired)).Should().Be(StankoHired);
        
        [Fact]
        public void Serializing_StudentMoved() =>
            DeserializeToDomainEvent(Serialize(StankoMovedToNoviSad)).Should().Be(StankoMovedToNoviSad);
    }
}