using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.BookEvents;
using LibraryEvents.UserEvents;
using static DomainServices.SagaCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.SagaSpecifications.LendBookSpecifications
{
    public sealed class WhenBookHasAlreadyBeenLentToAnotherUser : SpecificationFor<LendBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1IsAdded,
            JohnDoeUserIsAdded,
            WarAndPeace1IsLentToJohnDoe,
            JohnDoeBorrowedWarAndPeace1,
            StankoUserIsAdded);
        
        protected override LendBook AfterExecuting => new LendBook(WarAndPeace1Id, StankoId);

        protected override Func<LendBook, Task<Result>> By() => SagaCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue(),
            () => ProducedEvents.Should().NotContain(EventOf<BookLentToUser>()),
            () => ProducedEvents.Should().NotContain(EventOf<UserBorrowedBook>()));
    }
}