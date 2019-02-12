using Autofac;
using EventStore;
using InMemory;
using Ports.EventStore;

namespace AutofacApplicationWrapUp
{
    public sealed class EventStoreRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new EventStoreProvider("Culaja2")).As<IEventStore>().SingleInstance();
        }
    }
}