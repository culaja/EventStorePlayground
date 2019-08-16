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

namespace UnitTests.Specifications.BookSpecifications.ReturnBookSpecifications
{
    public sealed class WhenBookIsLentToUserButAnotherUserWantsToReturnIt : SpecificationFor<ReturnBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1Added,
            WarAndPeace1LentToJohnDoe);
        
        protected override ReturnBook AfterExecuting => new ReturnBook(WarAndPeace1Id, StankoId);

        protected override Func<ReturnBook, Task<Result>> By() => BookCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsFailure.Should().BeTrue(),
            () => ProducedEvents.Should().NotContain(EventOf<BookReturned>()));
    }
}