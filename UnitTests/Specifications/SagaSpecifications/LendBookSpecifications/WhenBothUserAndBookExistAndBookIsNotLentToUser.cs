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
    public sealed class WhenBothUserAndBookExistAndBookIsNotLentToUser : SpecificationFor<LendBook>
    {
        protected override IReadOnlyList<IDomainEvent> WhenGiven => Events(
            WarAndPeace1IsAdded,
            JohnDoeUserIsAdded);
        
        protected override LendBook AfterExecuting => new LendBook(WarAndPeace1Id, JohnDoeId);

        protected override Func<LendBook, Task<Result>> By() => SagaCommandExecutorsWith(Repository);
        
        protected override IReadOnlyList<Action> Outcome => Is(
            () => Result.IsSuccess.Should().BeTrue(),
            () => ProducedEvents.Should().Contain(WarAndPeace1IsLentToJohnDoe),
            () => ProducedEvents.Should().Contain(JohnDoeBorrowedWarAndPeace1));
    }
}