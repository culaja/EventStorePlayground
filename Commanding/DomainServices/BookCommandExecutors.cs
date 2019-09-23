using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain;
using Domain.Commands;
using Ports;

namespace DomainServices
{
    public static class BookCommandExecutors
    {
        public static Func<ICommand, Task<Result>> BookCommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case AddBook addBook:
                        return CreateBookExecutorWith(repository)(addBook);
                    case LendBook lendBook:
                        return LendBookExecutorWith(repository)(lendBook);
                    case ReturnBook returnBook:
                        return ReturnBookExecutorWith(repository)(returnBook);
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by {nameof(Book)} aggregate.");
                }
            };

        private static Func<ReturnBook, Task<Result>> ReturnBookExecutorWith(IRepository repository) =>
            c => repository.Borrow<Book>(c.BookId, book => book.ReturnFrom(c.UserId));

        private static Func<AddBook, Task<Result>> CreateBookExecutorWith(IRepository repository) => 
            c => repository.InsertNew(Book.NewBookFrom(c.BookId, c.BookName, c.YearOfPrint));

        private static Func<LendBook, Task<Result>> LendBookExecutorWith(IRepository repository) =>
            c => repository.Borrow<Book>(c.BookId, book => book.LendTo(c.UserId));
    }
}