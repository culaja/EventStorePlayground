using FluentAssertions;
using LibraryEvents.BookEvents;
using Xunit;
using static UnitTests.TestValues;

namespace UnitTests.DomainEventSerializationTests
{
    public sealed class BookDomainEventsSerializationTests
    {
        [Fact]
        public void book_added() => 
            WarAndPeace1IsAdded.Serialize().Deserialize<BookAdded>().Should().Be(WarAndPeace1IsAdded);

        [Fact]
        public void book_lent_to_user() =>
            WarAndPeace1IsLentToJohnDoe.Serialize().Deserialize<BookLentToUser>().Should()
                .Be(WarAndPeace1IsLentToJohnDoe);

        [Fact]
        public void book_returned() =>
            WarAndPeace1IsReturnedByJohnDoe.Serialize().Deserialize<BookReturned>().Should()
                .Be(WarAndPeace1IsReturnedByJohnDoe);
    }
}