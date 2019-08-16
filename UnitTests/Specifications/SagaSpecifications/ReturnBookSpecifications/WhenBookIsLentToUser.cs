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
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1IsAdded,
            JohnDoeUserIsAdded,
            WarAndPeace1IsLentToJohnDoe,
            JohnDoeBorrowedWarAndPeace1);
        
        protected override ReturnBook AfterExecuting => new ReturnBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<ReturnBook, Task<Result>> By() => SagaCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsSuccess.Should().BeTrue(),
            () => ProducedEvents.Should().Contain(WarAndPeace1IsReturnedByJohnDoe),
            () => ProducedEvents.Should().Contain(JohDoeFinishedWarAndPeace1Borrow));
    }
}