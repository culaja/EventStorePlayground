using System;
using Aggregate.Student.Shared;
using RabbitMqAdapter;

namespace EventPrinter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var eventSubscriber = new RabbitMqSubscriber("localhost");
            eventSubscriber.Register<StudentEventSubscription>(e => Console.WriteLine($"Received: {e}"));
            Console.ReadLine();
        }
    }
}