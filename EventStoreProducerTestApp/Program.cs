using System;
using EventStoreAdapter;
using EventStoreRepository;
using static Domain.Book.Book;
using static Domain.Book.BookId;
using static Domain.Book.YearOfPrint;

namespace EventStoreProducerTestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var repository = new Repository(
                new EventStoreAppender("tcp://localhost:1113", "Football"));

            var result = repository.InsertNew(NewBookFrom(BookIdFrom("Lord of the rings"), YearOfPrintFrom(2010))).Result;
            Console.WriteLine(result.IsSuccess);
        }
    }
}