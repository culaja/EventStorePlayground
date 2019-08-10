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
    public sealed class AddingABookWhenAnotherBookIsAdded : Specification<AddBook>
    {
        protected override AddBook CommandToExecute => 
            new AddBook(WarAndPeace2Id, WarAndPeaceName, YearOfPrint2010);
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return WarAndPeace1Added;
        }

        protected override Func<AddBook, Task<Result>> When() => CommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void book_is_added() => 
            ProducedEvents.Should().Contain(WarAndPeace2Added);
    }
}