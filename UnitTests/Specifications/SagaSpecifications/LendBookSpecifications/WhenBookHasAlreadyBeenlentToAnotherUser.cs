using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.BookEvents;
using LibraryEvents.UserEvents;
using Xunit;
using static DomainServices.SagaCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.SagaSpecifications.LendBookSpecifications
{
    public sealed class WhenBookHasAlreadyBeenLentToAnotherUser : SpecificationFor<LendBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
            yield return JohnDoeUserAdded;
            yield return WarAndPeace1LentToJohnDoe;
            yield return JohnDoeBorrowedWarAndPeace1;
            yield return StankoUserAdded;
        }
        
        protected override LendBook AfterExecuting => new LendBook(WarAndPeace1Id, StankoId);

        protected override Func<LendBook, Task<Result>> Through() => SagaCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void book_is_not_lent_again() => ProducedEvents.Should().NotContain(EventOf<BookLentToUser>());

        [Fact]
        public void user_has_not_borrowed_book() => ProducedEvents.Should().NotContain(EventOf<UserBorrowedBook>());
    }
}