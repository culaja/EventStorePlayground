using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using Ports;
using static DomainServices.BookCommandExecutors;

namespace DomainServices
{
    public static class DomainCommandExecutors
    {
        public static Func<ICommand, Task<Result>> CommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case SagaCommand sagaCommand:
                        return SagaCommandExecutors.SagaCommandExecutorsWith(repository)(c);
                    case BookCommand bookCommand:
                        return BookCommandExecutorsWith(repository)(c);
                    default:
                        throw new NotSupportedException($"Command '{c}' is not supported by the system.");
                }
            };
    }
}