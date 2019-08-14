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
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
            yield return WarAndPeace1LentToJohnDoe;
        }
        
        protected override ReturnBook AfterExecuting => new ReturnBook(WarAndPeace1Id, StankoId);

        protected override Func<ReturnBook, Task<Result>> Through() => BookCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void book_is_not_returned() => ProducedEvents.Should().NotContain(EventOf<BookReturned>());
    }
}