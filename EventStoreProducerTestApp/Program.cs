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
            var eventStore = new MyEventStore("tcp://localhost:1113", "Tenis");
            var repository = new Repository(eventStore);

            var result = repository.InsertNew(NewBallWith(BallIdFrom("2"), 5)
                .PassTo("Stanko")
                .Value).Result;
            Console.WriteLine(result.IsSuccess);
        }
    }
}