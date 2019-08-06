using System;
using EventStoreAdapter;
using EventStoreRepository;
using static Domain.Ball;
using static Domain.BallId;

namespace EventStoreProducerTestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var repository = new Repository(
                new EventStoreAppender("tcp://localhost:1113", "Football"));

            var result = repository.InsertNew(NewBallWith(BallIdFrom("2"), 10)
                .PassTo("Stanko")
                .Value).Result;
            Console.WriteLine(result.IsSuccess);
        }
    }
}