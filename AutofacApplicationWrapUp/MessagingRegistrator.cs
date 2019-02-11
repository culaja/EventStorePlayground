using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutofacMessageBus;
using Common.Messaging;
using CommonServices;
using Domain.StudentDomain;
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
                    typeof(IDomainEvent).Assembly,
                    typeof(Student).Assembly
                },
                new List<Assembly>()
                {
                    typeof(AggregateRootCreatedPersistenceHandler).Assembly,
                    typeof(AddNewStudentHandler).Assembly
                }));
        }

        private void RegisterOtherDomainServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AggregateConstructor>();
        }
    }
}