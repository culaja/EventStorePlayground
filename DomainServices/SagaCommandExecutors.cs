using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using Ports;
using static DomainServices.BookCommandExecutors;
using static DomainServices.UserCommandExecutors;

namespace DomainServices
{
    public static class SagaCommandExecutors
    {
        public static Func<ICommand, Task<Result>> SagaCommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case LendBook lendBook:
                        return LendBookExecutorWith(repository)(lendBook);
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by {nameof(SagaCommandExecutors)}.");
                }
            };

        private static Func<LendBook, Task<Result>> LendBookExecutorWith(IRepository repository) =>
            c => BookCommandExecutorsWith(repository)(c)
                .OnSuccess(() => UserCommandExecutorsWith(repository)(c));
    }
}