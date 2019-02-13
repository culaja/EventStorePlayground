using System;
using Domain.StudentDomain;
using Domain.StudentDomain.Events;
using RabbitMqMessageBus;

namespace EventPrinter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var eventSubscriber = new RemoteMessageBus(new RabbitMqServerConfiguration("localhost"));
            eventSubscriber.SubscribeTo<Student, StudentEvent>(e => Console.WriteLine($"Received: {e}"));
            Console.ReadLine();
        }
    }
}