using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using Xunit;
using static DomainServices.BookCommandExecutors;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.BookSpecifications.LendBookSpecifications
{
    public sealed class LendingABookWhenBookIsNotLent : SpecificationFor<LendBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
        }
        
        protected override LendBook AfterExecutingCommand => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> Through() => BookCommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void book_is_lent_to_JohnDoe() => ProducedEvents.Should().Contain(WarAndPeace1LentToJohnDoe);
    }
}