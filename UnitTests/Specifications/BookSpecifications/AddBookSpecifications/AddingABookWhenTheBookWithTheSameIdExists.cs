using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using Xunit;
using static DomainServices.DomainCommandExecutors;
using static UnitTests.BookTestValues;

namespace UnitTests.Specifications.BookSpecifications.AddBookSpecifications
{
    public sealed class AddingABookWhenTheBookWithTheSameIdExists : Specification<AddBook>
    {
        protected override AddBook CommandToExecute => new AddBook(WarAndPeace1Id, WarAndPeaceName, YearOfPrint2010);
        
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return WarAndPeace1Added;
        }

        protected override Func<AddBook, Task<Result>> When() => CommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}