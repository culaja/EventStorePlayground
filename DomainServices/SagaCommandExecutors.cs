using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain;
using Domain.Commands;
using Ports;

namespace DomainServices
{
    internal static class SagaCommandExecutors
    {
        public static Func<ICommand, Task<Result>> SagaCommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case LendBook lendBook:
                        return LendBookExecutorWith(repository)(lendBook);
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by any Saga command executor.");
                }
            };
        
        private static Func<LendBook, Task<Result>> LendBookExecutorWith(IRepository repository) => 
            c => repository.Borrow<Book>(c.BookToLendId, book => book.LendTo(c.BorrowerUserId));
    }
}