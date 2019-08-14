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

namespace UnitTests.Specifications.BookSpecifications.AddBookSpecifications
{
    public sealed class AddingABookWhenTheBookWithTheSameIdExists : SpecificationFor<AddBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
        }
        
        protected override AddBook AfterExecutingCommand => new AddBook(WarAndPeace1Id, WarAndPeaceName, YearOfPrint2010);

        protected override Func<AddBook, Task<Result>> Through() => BookCommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}