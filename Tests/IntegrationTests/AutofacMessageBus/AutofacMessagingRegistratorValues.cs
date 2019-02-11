using System.Collections.Generic;
using System.Reflection;
using AutofacMessageBus;
using Common.Messaging;
using CommonServices;
using Domain.StudentDomain;
using DomainServices.StudentHandlers.Commands;

namespace Tests.IntegrationTests.AutofacMessageBus
{
    public static class AutofacMessagingRegistratorValues
    {
        public static readonly AutofacMessagingRegistrator CompleteAutofacMessagingRegistrator = new AutofacMessagingRegistrator(
            new List<Assembly>
            {
                typeof(IDomainEvent).Assembly,
                typeof(Student).Assembly
            },
            new List<Assembly>()
            {
                typeof(AggregateRootCreatedPersistenceHandler).Assembly,
                typeof(AddNewStudentHandler).Assembly
            });
    }
}