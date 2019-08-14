using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.UserEvents;
using Xunit;
using static DomainServices.SagaCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.SagaSpecifications.LendBookSpecifications
{
    public sealed class WhenUserHasAlreadyBorrowedAnotherBook : SpecificationFor<LendBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
            yield return JohnDoeUserAdded;
            yield return WarAndPeace1LentToJohnDoe;
            yield return JohnDoeBorrowedWarAndPeace1;
            yield return WarAndPeace2Added;
        }
        
        protected override LendBook AfterExecutingCommand => new LendBook(WarAndPeace2Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> Through() => SagaCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void book_has_been_lent_to_user() => ProducedEvents.Should().Contain(WarAndPeace2LentToJohnDoe);

        [Fact]
        public void book_is_returned() => ProducedEvents.Should().Contain(WarAndPeace2IsReturnedByJohnDoe);

        [Fact]
        public void user_has_not_borrowed_the_book() => ProducedEvents.Should().NotContain(EventOf<UserBorrowedBook>());
    }
}