using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using Ports;
using static DomainServices.BallCommandExecutors;

namespace DomainServices
{
    public static class DomainCommandExecutors
    {
        public static Func<ICommand, Task<Result>> CommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case BallCommand ballCommand:
                        return BallCommandExecutorsWith(repository)(c);
                    default:
                        throw new NotSupportedException($"Command '{c}' is not supported by the system.");
                }
            };
    }
}