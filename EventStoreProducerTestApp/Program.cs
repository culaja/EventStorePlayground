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
            var eventStore = new MyEventStore("tcp://localhost:1113", "Football");
            
            var ball1Id = BallId.BallIdFrom("1");

//            var events = eventStore.LoadAllEventsForAsync(ball1Id).Result;
//            int a = 1;

            eventStore.AppendAsync(
                ball1Id, 
                new IDomainEvent[]
                {
                    new BallCreated(ball1Id, 5),
                    new BallPassed(ball1Id, "Stanko", "Milenko"),
                    new BallPassed(ball1Id, "Milenko", "Danijel"),
                },
                -1).Wait();
        }
    }
}