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
        var eventStore = new EventStoreAppender("tcp://localhost:1113", "Football");
            var repository = new Repository(eventStore);

            var result = repository.InsertNew(NewBallWith(BallIdFrom("2"), 10)
                .PassTo("Stanko")
                .Value).Result;
            Console.WriteLine(result.IsSuccess);
        }
    }
}