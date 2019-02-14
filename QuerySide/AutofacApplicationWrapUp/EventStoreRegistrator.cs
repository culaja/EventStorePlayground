using Autofac;
using EventStore;
using Ports;
using RabbitMqAdapter;

namespace AutofacApplicationWrapup
{
    public sealed class EventStoreRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new DatabaseContext("mongodb://localhost:27017/", "EventStore1")).SingleInstance();
            builder.RegisterType<EventStoreReader>().As<IEventStoreReader>().SingleInstance();
        }
    }

    public sealed class RemoteMessagingRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new RabbitMqSubscriber("localhost")).As<IRemoteEventSubscriber>().SingleInstance();
        }
    }
}