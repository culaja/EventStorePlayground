using System;
using Domain;
using LibraryEvents.BookEvents;
using EventStoreAdapter.Reading;

namespace EventStoreSubscriptionsTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStoreSubscriber = new EventStoreSubscriber("tcp://localhost:1113", "Library");

            var bookAddedEventsSubscription = eventStoreSubscriber.SubscribeToEventsOfType<BookAdded>();
            var allBooksEventsSubscription = eventStoreSubscriber.SubscribeToAggregateTypeEvents<Book>();
            var book1Events = eventStoreSubscriber.SubscribeToAggregateEvents(BookId.BookIdFrom("1"));
            
            using (new DomainEventStreamConsoleConsumer(nameof(bookAddedEventsSubscription), bookAddedEventsSubscription.Stream, ""))
            using (new DomainEventStreamConsoleConsumer(nameof(allBooksEventsSubscription), allBooksEventsSubscription.Stream, "\t\t\t\t\t\t\t\t\t"))
            using (new DomainEventStreamConsoleConsumer(nameof(book1Events), book1Events.Stream, "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t"))
            {
                Console.ReadLine();
                bookAddedEventsSubscription.Stop();
                allBooksEventsSubscription.Stop();
                book1Events.Stop();
            }
        }
    }
}