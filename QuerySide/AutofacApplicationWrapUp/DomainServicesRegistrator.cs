using System.Collections.Generic;
using System.Reflection;
using Aggregate.Student.Shared;
using Autofac;
using AutofacMessageBus;
using Services;
using Services.EventHandlers;
using Module = Autofac.Module;

namespace AutofacApplicationWrapup
{
    public sealed class DomainServicesRegistrator : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            RegisterAllMessageHandlersForAllDomainMessages(containerBuilder);
            RegisterOtherDomainServices(containerBuilder);
        }

        private void RegisterAllMessageHandlersForAllDomainMessages(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new AutofacMessagingRegistrator(
                new List<Assembly>
                {
                    typeof(StudentEvent).Assembly
                },
                new List<Assembly>()
                {
                    typeof(ViewRefreshFromStudentEventHandler).Assembly
                }));
        }

        private void RegisterOtherDomainServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<QuerySideInitializer>();
        }
    }
}