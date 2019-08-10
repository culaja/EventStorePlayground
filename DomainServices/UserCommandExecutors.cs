using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain;
using Domain.Commands;
using Ports;

namespace DomainServices
{
    internal static class UserCommandExecutors
    {
        public static Func<ICommand, Task<Result>> UserCommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case AddUser addUser:
                        return AddUserExecutorWith(repository)(addUser);
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by {nameof(User)} aggregate.");
                }
            };
        
        private static Func<AddUser, Task<Result>> AddUserExecutorWith(IRepository repository) => 
            c => repository.InsertNew(User.NewUserFrom(c.UserId, c.FullName));
    }
}