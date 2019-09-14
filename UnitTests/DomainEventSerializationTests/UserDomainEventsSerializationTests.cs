using FluentAssertions;
using LibraryEvents.UserEvents;
using Xunit;
using static UnitTests.TestValues;

namespace UnitTests.DomainEventSerializationTests
{
    public sealed class UserDomainEventsSerializationTests
    {
        [Fact]
        public void user_added() =>
            JohnDoeUserIsAdded.Serialize().Deserialize<UserAdded>().Should().Be(JohnDoeUserIsAdded);

        [Fact]
        public void user_borrowed_book() =>
            JohnDoeBorrowedWarAndPeace1.Serialize().Deserialize<UserBorrowedBook>().Should()
                .Be(JohnDoeBorrowedWarAndPeace1);

        [Fact]
        public void user_finished_book_borrow() =>
            JohDoeFinishedWarAndPeace1Borrow.Serialize().Deserialize<UserFinishedBookBorrow>().Should()
                .Be(JohDoeFinishedWarAndPeace1Borrow);
    }
}