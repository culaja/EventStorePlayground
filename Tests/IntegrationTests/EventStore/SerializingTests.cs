using FluentAssertions;
using Xunit;
using static EventStore.DomainEventSerializer;
using static Tests.SomeStudentEvents;

namespace Tests.IntegrationTests.EventStore
{
    public class SerializingTests
    {
        [Fact]
        public void Serializing_StudentHired() =>
            DeserializeToDomainEvent(Serialize(StankoHired)).Should().Be(StankoHired);
        
        [Fact]
        public void Serializing_StudentMoved() =>
            DeserializeToDomainEvent(Serialize(StankoMoved)).Should().Be(StankoMoved);
    }
}