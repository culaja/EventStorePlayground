using Domain;
using FluentAssertions;
using Xunit;
using static Domain.UserId;

namespace UnitTests.ValueObjectValidationTests
{
    public sealed class UserIdTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("culaja")]
        [InlineData("john.doe")]
        public void for_invalid_input_throws_InvalidUserIdException(string userIdString) =>
            Assert.Throws<InvalidUserIdException>(() => UserIdFrom(userIdString));

        [Theory]
        [InlineData("culaja@gmail.com")]
        [InlineData("john.doe@example.com")]
        public void for_valid_input_valid_UserId_is_created(string userIdString) =>
            UserIdFrom(userIdString).ToString().Should().Be(userIdString);
    }
}