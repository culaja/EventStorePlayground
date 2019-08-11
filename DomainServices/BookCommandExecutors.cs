﻿using System;
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
                    default:
                        throw new NotSupportedException($"Command '{c}' can't be handled by {nameof(Book)} aggregate.");
                }
            };
        
        private static Func<AddBook, Task<Result>> CreateBookExecutorWith(IRepository repository) => 
            c => repository.InsertNew(Book.NewBookFrom(c.BookId, c.BookName, c.YearOfPrint));

        private static Func<LendBook, Task<Result>> LendBookExecutorWith(IRepository repository) =>
            c => repository.Borrow<Book>(c.BookToLendId, book => book.LendTo(c.BorrowerUserId));
    }
}