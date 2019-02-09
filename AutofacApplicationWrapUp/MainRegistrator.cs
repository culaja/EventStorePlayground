using Autofac;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp
{
    public sealed class MainRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<MessagingRegistrator>();
            builder.RegisterModule<EventStoreRegistrator>();
            builder.RegisterModule<RepositoryRegistrator>();
        }
    }
}