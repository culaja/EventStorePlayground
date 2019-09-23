using Common;
using FluentAssertions;
using Xunit;

namespace UnitTests.ValueObjectValidationTests
{
    public sealed class AnyAggregateIdTests
    {
        private sealed class TestAggregateId : AggregateId
        {
            public TestAggregateId(string aggregateIdType, string aggregateId) : base(aggregateIdType, aggregateId)
            {
            }
        }
        
        [Theory]
        [InlineData("TestType", "\t")]
        [InlineData("TestType", "   ")]
        [InlineData("TestType", "abc\nf")]
        [InlineData("TestType", " abc")]
        [InlineData("TestType", "a bc")]
        [InlineData("TestType", "abc_")]
        [InlineData("TestType", "ab_c")]
        [InlineData("TestType", "_abc")]
        [InlineData("\t", "AnyAggregate")]
        [InlineData("   ", "AnyAggregate")]
        [InlineData("abc\nf", "AnyAggregate")]
        [InlineData(" abc", "AnyAggregate")]
        [InlineData("a bc", "AnyAggregate")]
        [InlineData("abc_", "AnyAggregate")]
        [InlineData("ab_c", "AnyAggregate")]
        [InlineData("_abc", "AnyAggregate")]
        public void for_invalid_input_throws_InvalidAggregateIdException(string aggregateTypeString, string aggregateIdString) =>
            Assert.Throws<InvalidAggregateIdException>(() => new TestAggregateId(aggregateTypeString, aggregateIdString));

        [Theory]
        [InlineData("User", "culaja@gmail.com")]
        [InlineData("Book", "WarAndPeace1")]
        public void for_valid_input_valid_aggregate_id_is_created(string aggregateTypeString,
            string aggregateIdString) =>
            new TestAggregateId(aggregateTypeString, aggregateIdString).ToString().Should().Be(aggregateIdString);
    }
}