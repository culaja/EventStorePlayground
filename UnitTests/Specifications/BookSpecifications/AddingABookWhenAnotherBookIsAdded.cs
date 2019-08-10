using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Book;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.BookEvents;
using Xunit;
using static DomainServices.DomainCommandExecutors;

namespace UnitTests.Specifications.BookSpecifications
{
    public sealed class AddingABookWhenAnotherBookIsAdded : Specification<AddBook>
    {
        protected override AddBook CommandToExecute => new AddBook(BookId.BookIdFrom("2"), BookName.BookNameFrom("Lord of the rings"), YearOfPrint.YearOfPrintFrom(2010));
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return new BookAdded(BookId.BookIdFrom("1"), BookName.BookNameFrom("Lord of the rings"), YearOfPrint.YearOfPrintFrom(2010));
        }

        protected override Func<AddBook, Task<Result>> When() => CommandExecutorsWith(Repository);

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();

        [Fact]
        public void book_is_added() => 
            ProducedEvents.Should().Contain(new BookAdded("2", "Lord of the rings", 2010));
    }
}