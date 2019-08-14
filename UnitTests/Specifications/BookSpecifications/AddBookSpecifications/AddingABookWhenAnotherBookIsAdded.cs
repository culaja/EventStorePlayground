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
    public sealed class AddingABookWhenAnotherBookIsAdded : SpecificationFor<AddBook>
    {
        protected override IEnumerable<IDomainEvent> WhenGiven()
        {
            yield return WarAndPeace1Added;
        }
        
        protected override AddBook AfterExecuting => new AddBook(WarAndPeace2Id, WarAndPeaceName, YearOfPrint2010);

        protected override Func<AddBook, Task<Result>> Through() => BookCommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void book_is_added() => 
            ProducedEvents.Should().Contain(WarAndPeace2Added);
    }
}