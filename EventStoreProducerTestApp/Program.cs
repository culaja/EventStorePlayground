using System;
using Common;
using Common.Messaging;
using EventStoreAdapter;
using EventStoreRepository;
using LocalMessageBusAdapter;
using static Domain.Ball;
using static Domain.BallId;

namespace EventStoreProducerTestApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var localMessageBus = new LocalMessageBus(WriteMessageHandler))
            {
                var eventStore = new EventStoreAppender("tcp://localhost:1113", "Football", localMessageBus);
                var repository = new Repository(eventStore);

                var result = repository.InsertNew(NewBallWith(BallIdFrom("2"), 10)
                    .PassTo("Stanko")
                    .Value).Result;
                Console.WriteLine(result.IsSuccess);
            }
        }

        private static Result WriteMessageHandler(IMessage message)
        {
            Console.WriteLine(message);
            return Result.Ok();
        }
    }
}