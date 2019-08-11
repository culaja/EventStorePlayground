using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.UserEvents;
using Xunit;
using static DomainServices.UserCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.UserSpecification.LendBookSpecifications
{
    public sealed class WhenUserHasAlreadyLentBook : Specification<LendBook>
    {
        protected override LendBook CommandToExecute => new LendBook(WarAndPeace1Id, JohnDoeId);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return JohnDoeUserAdded;
            yield return JohnDoeBorrowedWarAndPeace1;
        }

        protected override Func<LendBook, Task<Result>> When() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void user_has_not_borrowed_book() =>
            ProducedEvents.Should().NotContain(EventOf<UserBorrowedBook>());
    }
}