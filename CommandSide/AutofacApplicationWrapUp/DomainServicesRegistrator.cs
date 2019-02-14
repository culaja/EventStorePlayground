using System.Collections.Generic;
using System.Reflection;
using Aggregate.Student.Shared;
using Autofac;
using AutofacMessageBus;
using DomainServices;
using DomainServices.StudentHandlers.Commands;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp
{
    public class DomainServicesRegistrator : Module
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
                    typeof(StudentCreated).Assembly
                },
                new List<Assembly>()
                {
                    typeof(AddNewStudentHandler).Assembly
                }));
        }

        private void RegisterOtherDomainServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AggregateConstructor>();
        }
    }
}