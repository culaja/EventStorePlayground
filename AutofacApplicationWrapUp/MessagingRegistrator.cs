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
    public class MessagingRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacMessagingRegistrator(
                new List<Assembly>
                {
                    typeof(IDomainEvent).Assembly,
                    typeof(Student).Assembly
                },
                new List<Assembly>()
                {
                    typeof(AddNewStudentHandler).Assembly,
                    typeof(DomainEventToEventStoreHandler).Assembly
                }));
        }
    }
}