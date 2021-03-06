﻿using System;
using Domain;
using EventStoreAdapter.Writing;
using EventStoreRepository;
using static Domain.Book;
using static Domain.BookId;
using static Domain.YearOfPrint;

namespace EventStoreProducerTestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var repository = new Repository(
                new EventStoreAppender("tcp://localhost:1113", "Library"));

            var result = repository.InsertNew(NewBookFrom(BookIdFrom("2"), BookName.BookNameFrom("Lord of the rings 1"), YearOfPrintFrom(2010))).Result;
            Console.WriteLine(result.IsSuccess);
        }
    }
}