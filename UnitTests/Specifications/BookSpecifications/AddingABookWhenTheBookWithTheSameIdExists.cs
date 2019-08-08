using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using FluentAssertions;
using LibraryEvents.BookEvents;
using Xunit;
using static Domain.Book.BookId;
using static Domain.Book.BookName;
using static Domain.Book.YearOfPrint;
using static DomainServices.DomainCommandExecutors;

namespace UnitTests.Specifications.BookSpecifications
{
    public sealed class AddingABookWhenTheBookWithTheSameIdExists : Specification<AddBook>
    {
        protected override AddBook CommandToExecute => new AddBook(BookIdFrom("1"), BookNameFrom("Lord of the rings"), YearOfPrintFrom(2010));
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return new BookAdded(BookIdFrom("1"), BookNameFrom("Lord of the rings"), YearOfPrintFrom(2010));
        }

        protected override Func<AddBook, Task<Result>> When() => CommandExecutorsWith(Repository);

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}