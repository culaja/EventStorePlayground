using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain;
using Domain.Commands;
using Ports;

namespace DomainServices
{
    internal static class BookCommandExecutors
    {
        public static Func<ICommand, Task<Result>> BookCommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case AddBook addBook:
                        return CreateBookExecutorWith(repository)(addBook);
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by {nameof(Book)} aggregate.");
                }
            };
        
        private static Func<AddBook, Task<Result>> CreateBookExecutorWith(IRepository repository) => 
            c => repository.InsertNew(Book.NewBookFrom(c.BookId, c.BookName, c.YearOfPrint));
    }
}