using Autofac;
using InMemory;
using Ports.EventStore;

namespace AutofacApplicationWrapUp
{
    public sealed class EventStoreRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryEventStore>().As<IEventStore>().SingleInstance();
        }
    }
}