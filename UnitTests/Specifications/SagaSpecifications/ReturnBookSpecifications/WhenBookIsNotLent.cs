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

namespace UnitTests.Specifications.SagaSpecifications.ReturnBookSpecifications
{
    public sealed class WhenBookIsNotLent : SpecificationFor<ReturnBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
            yield return JohnDoeUserAdded;
        }
        
        protected override ReturnBook AfterExecuting => new ReturnBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<ReturnBook, Task<Result>> By() => SagaCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();

        [Fact]
        public void book_is_not_returned() => ProducedEvents.Should().NotContain(EventOf<BookReturned>());
        
        [Fact]
        public void borrow_is_not_finished() => ProducedEvents.Should().NotContain(EventOf<UserFinishedBookBorrow>());
    }
}