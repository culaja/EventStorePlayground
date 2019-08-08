using System;
using LibraryEvents.BookEvents;
using Domain.Book;
using EventStoreAdapter.Reading;

namespace EventStoreSubscriptionsTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStoreSubscriber = new EventStoreSubscriber("tcp://localhost:1113", "Football");

            var ballCreatedEventsSubscription = eventStoreSubscriber.SubscribeToEventsOfType<BookAdded>();
            var allBallEventsSubscription = eventStoreSubscriber.SubscribeToAggregateTypeEvents<Book>();
            var lordOfTheRingsEvents = eventStoreSubscriber.SubscribeToAggregateEvents<Book>(BookId.BookIdFrom("Lord of the rings"));
            
            using (new DomainEventStreamConsoleConsumer(nameof(ballCreatedEventsSubscription), ballCreatedEventsSubscription.Stream, ""))
            using (new DomainEventStreamConsoleConsumer(nameof(allBallEventsSubscription), allBallEventsSubscription.Stream, "\t\t\t\t\t\t\t\t\t"))
            using (new DomainEventStreamConsoleConsumer(nameof(lordOfTheRingsEvents), lordOfTheRingsEvents.Stream, "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"))
            {
                Console.ReadLine();
                ballCreatedEventsSubscription.Stop();
                allBallEventsSubscription.Stop();
                lordOfTheRingsEvents.Stop();
            }
        }
    }
}