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

namespace UnitTests.Specifications.SagaSpecifications.ReturnBookSpecifications
{
    public sealed class WhenBookIsLentToUser : SpecificationFor<ReturnBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
            yield return JohnDoeUserAdded;
            yield return WarAndPeace1LentToJohnDoe;
            yield return JohnDoeBorrowedWarAndPeace1;
        }
        
        protected override ReturnBook AfterExecutingCommand => new ReturnBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<ReturnBook, Task<Result>> Through() => SagaCommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void book_is_returned() => ProducedEvents.Should().Contain(WarAndPeace1IsReturnedByJohnDoe);

        [Fact]
        public void user_finished_borrow() => ProducedEvents.Should().Contain(JohDoeFinishedWarAndPeace1Borrow);
    }
}