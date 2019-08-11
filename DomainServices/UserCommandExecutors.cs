using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain;
using Domain.Commands;
using Ports;

namespace DomainServices
{
    public static class UserCommandExecutors
    {
        public static Func<ICommand, Task<Result>> UserCommandExecutorsWith(IRepository repository) =>
            c =>
            {
                switch (c)
                {
                    case AddUser addUser:
                        return AddUserExecutorWith(repository)(addUser);
                    case LendBook lendBook:
                        return LendBookExecutorWith(repository)(lendBook);
                    case ReturnBook returnBook:
                        return ReturnBookExecutorWith(repository)(returnBook);
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by {nameof(User)} aggregate.");
                }
            };
        
        private static Func<AddUser, Task<Result>> AddUserExecutorWith(IRepository repository) => 
            c => repository.InsertNew(User.NewUserFrom(c.UserId, c.FullName));
        
        private static Func<LendBook, Task<Result>> LendBookExecutorWith(IRepository repository) => 
            c => repository.Borrow<User>(c.BorrowerUserId, user => user.BorrowBook(c.BookToLendId));
        
        private static Func<ReturnBook, Task<Result>> ReturnBookExecutorWith(IRepository repository) => 
            c => repository.Borrow<User>(c.UserId, user => user.FinishBorrowOf(c.BookId));
    }
}