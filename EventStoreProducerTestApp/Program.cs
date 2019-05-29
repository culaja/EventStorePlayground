using BallEvents;
using Common.Messaging;
using Domain;
using EventStoreAdapter;

namespace EventStoreProducerTestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var eventStore = new MyEventStore("tcp://localhost:1113");
            
            var ball1Id = BallId.BallIdFrom("1");

            var events = eventStore.LoadAllEventsForAsync(ball1Id).Result;
            int a = 1;

//            eventStore.AppendAsync(ball1Id, new IDomainEvent[]
//            {
//                new BallCreated(ball1Id, 5).SetVersion(9),
//                new BallPassed(ball1Id, "Stanko", "Milenko").SetVersion(10),
//                new BallPassed(ball1Id, "Milenko", "Danijel").SetVersion(11),
//            }).Wait();
        }
    }
}