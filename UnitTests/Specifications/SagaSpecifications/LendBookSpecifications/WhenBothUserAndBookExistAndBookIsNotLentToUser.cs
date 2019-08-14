using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using Xunit;
using static DomainServices.SagaCommandExecutors;
using static UnitTests.TestValues;

namespace UnitTests.Specifications.SagaSpecifications.LendBookSpecifications
{
    public sealed class WhenBothUserAndBookExistAndBookIsNotLentToUser : Specification<LendBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
            yield return JohnDoeUserAdded;
        }
        
        protected override LendBook AfterExecutingCommand => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> Through() => SagaCommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void book_is_lent_to_user() => ProducedEvents.Should().Contain(WarAndPeace1LentToJohnDoe);

        [Fact]
        public void user_borrowed_book() => ProducedEvents.Should().Contain(JohnDoeBorrowedWarAndPeace1);
    }
}