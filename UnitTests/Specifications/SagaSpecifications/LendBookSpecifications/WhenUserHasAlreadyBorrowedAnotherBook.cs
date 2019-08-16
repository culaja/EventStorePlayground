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
        
        protected override LendBook AfterExecuting => new LendBook(WarAndPeace2Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> By() => SagaCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue(),
            () => ProducedEvents.Should().Contain(WarAndPeace2LentToJohnDoe),
            () => ProducedEvents.Should().Contain(WarAndPeace2IsReturnedByJohnDoe),
            () => ProducedEvents.Should().NotContain(EventOf<UserBorrowedBook>()));
    }
}