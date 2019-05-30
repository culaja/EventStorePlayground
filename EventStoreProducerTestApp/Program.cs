using System;
using BallEvents;
using Common;
using Common.Messaging;
using Domain;
using EventStoreAdapter;
using EventStoreRepository;

namespace EventStoreProducerTestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var eventStore = new MyEventStore("tcp://localhost:1113", "Football");
            var repository = new Repository(eventStore);
            
            var ball1Id = BallId.BallIdFrom("1");

            var ballResult = repository.BorrowBy<Ball>(ball1Id, ball =>
            {
                Console.WriteLine(ball);
                return Result.Ok(ball);
            }).Result;

//            eventStore.AppendAsync(
//                ball1Id, 
//                new IDomainEvent[]
//                {
//                    new BallCreated(ball1Id, 5),
//                    new BallPassed(ball1Id, "Stanko", "Milenko"),
//                    new BallPassed(ball1Id, "Milenko", "Danijel"),
//                },
//                -1).Wait();
        }
    }
}