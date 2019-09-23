using Domain;
using FluentAssertions;
using Xunit;
using static Domain.YearOfPrint;

namespace UnitTests.ValueObjectValidationTests
{
    public sealed class YearOfPrintTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1899)]
        [InlineData(2020)]
        public void for_invalid_input_throws_InvalidYearOfPrintException(int yearOfPrint) =>
            Assert.Throws<InvalidYearOfPrintException>(() => YearOfPrintFrom(yearOfPrint));
        
        [Theory]
        [InlineData(1900)]
        [InlineData(2010)]
        [InlineData(2019)]
        public void for_valid_input_valid_YearOfPrint_is_created(int yearOfPrint) =>
            ((int)YearOfPrintFrom(yearOfPrint)).Should().Be(yearOfPrint);

    }
}