using Domain;
using FluentAssertions;
using Xunit;
using static Domain.BookName;

namespace UnitTests.ValueObjectValidationTests
{
    public sealed class BookNameTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void for_invalid_input_throws_InvalidBookNameException(string bookNameString) =>
            Assert.Throws<InvalidBookNameException>(() => BookNameFrom(bookNameString));

        [Theory]
        [InlineData("WarAndPeace1")]
        public void for_valid_input_valid_BookName_is_created(string bookNameString) =>
            BookNameFrom(bookNameString).ToString().Should().Be(bookNameString);
    }
}