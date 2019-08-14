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
    public sealed class LendingABookWhenBookIsAlreadyLent : Specification<LendBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
            yield return WarAndPeace1LentToJohnDoe;
        }
        
        protected override LendBook AfterExecutingCommand => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> Through() => BookCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void book_is_not_lent() => ProducedEvents.Should().NotContain(EventOf<BookLentToUser>());
    }
}