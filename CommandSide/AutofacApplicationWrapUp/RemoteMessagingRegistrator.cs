using Autofac;
using Ports.Messaging;
using RabbitMqMessageBus;

namespace AutofacApplicationWrapUp
{
    public sealed class RemoteMessagingRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new RemoteMessageBus(new RabbitMqServerConfiguration("localhost")))
                .As<IRemoteMessageBus>()
                .SingleInstance();
        }
    }
}