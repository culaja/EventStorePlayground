using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.BookEvents;
using Xunit;
using static DomainServices.BookCommandExecutors;
using static UnitTests.AssertionsHelpers;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.BookSpecifications.LendBookSpecifications
{
    public sealed class LendingABookWhenBookIsAlreadyLent : SpecificationFor<LendBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1IsAdded,
            WarAndPeace1IsLentToJohnDoe);
        
        protected override LendBook AfterExecuting => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> By() => BookCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue(),
            () => ProducedEvents.Should().NotContain(EventOf<BookLentToUser>()));
    }
}