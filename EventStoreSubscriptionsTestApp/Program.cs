using System;
using BallEvents;
using Domain;
using EventStoreAdapter;
using EventStoreAdapter.Reading;
using static Domain.BallId;

namespace EventStoreSubscriptionsTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStoreSubscriber = new EventStoreSubscriber("tcp://127.0.0.1:1113", "Football");

            var ballCreatedEventsSubscription = eventStoreSubscriber.SubscribeToEventsOfType<BallCreated>();
            var allBallEventsSubscription = eventStoreSubscriber.SubscribeToAggregateTypeEvents<Ball>();
            var ball5Events = eventStoreSubscriber.SubscribeToAggregateEvents<Ball>(BallIdFrom("5"));
            
            using (new DomainEventStreamConsoleConsumer(nameof(ballCreatedEventsSubscription), ballCreatedEventsSubscription.Stream, ""))
            using (new DomainEventStreamConsoleConsumer(nameof(allBallEventsSubscription), allBallEventsSubscription.Stream, "\t\t\t\t\t\t\t\t\t"))
            using (new DomainEventStreamConsoleConsumer(nameof(ball5Events), ball5Events.Stream, "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"))
            {
                Console.ReadLine();
                ballCreatedEventsSubscription.Stop();
                allBallEventsSubscription.Stop();
                ball5Events.Stop();
            }
        }
    }
}