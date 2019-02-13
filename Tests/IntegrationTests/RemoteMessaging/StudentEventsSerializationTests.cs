using FluentAssertions;
using RabbitMqMessageBus.Mappings;
using Xunit;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.RemoteMessaging
{
    public sealed class StudentEventsSerializationTests
    {
        [Fact]
        public void StudentCreated() => 
            StankoCreated.Serialize().Deserialize().Value.Should().Be(StankoCreated);
        
        [Fact]
        public void StudentMoved() => 
            StankoMovedToNoviSad.Serialize().Deserialize().Value.Should().Be(StankoMovedToNoviSad);
        
        [Fact]
        public void StudentHired() => 
            StankoHired.Serialize().Deserialize().Value.Should().Be(StankoHired);
    }
}