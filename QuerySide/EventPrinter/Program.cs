using System;
using Aggregate.Student.Shared;
using RabbitMqAdapter;

namespace EventPrinter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var studentPerCityView = new StudentsPerCityView();
            var eventSubscriber = new RabbitMqSubscriber("localhost");
            eventSubscriber.Register<StudentEventSubscription>(e =>
            {
                studentPerCityView.Apply(e);
                Console.WriteLine(studentPerCityView);
            });
            Console.ReadLine();
        }
    }
}