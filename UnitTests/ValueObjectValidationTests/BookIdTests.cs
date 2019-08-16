using Domain;
using FluentAssertions;
using Xunit;
using static Domain.BookId;

namespace UnitTests.ValueObjectValidationTests
{
    public sealed class BookIdTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void for_invalid_input_throws_InvalidBookIdException(string bookIdString) =>
            Assert.Throws<InvalidBookIdException>(() => BookIdFrom(bookIdString));

        [Theory]
        [InlineData("WarAndPeace1")]
        public void for_valid_input_valid_BookId_is_created(string bookIdString) =>
            BookIdFrom(bookIdString).ToString().Should().Be(bookIdString);
    }
}