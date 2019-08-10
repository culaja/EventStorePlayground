using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using Xunit;
using static DomainServices.DomainCommandExecutors;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.BookSpecifications.LendBookSpecifications
{
    public sealed class LendingABookWhenBookIsNotLend : Specification<LendBook>
    {
        protected override LendBook CommandToExecute => new LendBook(WarAndPeace1Id, JohnDoeId);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return WarAndPeace1Added;
        }

        protected override Func<LendBook, Task<Result>> When() => CommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void book_is_lent_to_JohnDoe() => ProducedEvents.Should().Contain(WarAndPeaceLentToJohnDoe);
    }
}