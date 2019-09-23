using Domain;
using FluentAssertions;
using Xunit;
using static Domain.FullName;

namespace UnitTests.ValueObjectValidationTests
{
    public sealed class FullNameTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void for_invalid_input_throws_InvalidFullNameException(string fullNameString) =>
            Assert.Throws<InvalidFullNameException>(() => FullNameFrom(fullNameString));

        [Theory]
        [InlineData("John Doe")]
        [InlineData("Stanko Culaja")]
        public void for_valid_input_valid_FullName_is_created(string fullNameString) =>
            FullNameFrom(fullNameString).ToString().Should().Be(fullNameString);
    }
}