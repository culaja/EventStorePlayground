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
    public sealed class WhenUserHasAlreadyLentBook : SpecificationFor<LendBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return JohnDoeUserAdded;
            yield return JohnDoeBorrowedWarAndPeace1;
        }
        
        protected override LendBook AfterExecutingCommand => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> Through() => UserCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void user_has_not_borrowed_book() =>
            ProducedEvents.Should().NotContain(EventOf<UserBorrowedBook>());
    }
}