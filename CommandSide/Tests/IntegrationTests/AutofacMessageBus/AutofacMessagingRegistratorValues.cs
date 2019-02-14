using System.Collections.Generic;
using System.Reflection;
using Aggregate.Student.Shared;
using AutofacMessageBus;
using DomainServices.StudentHandlers.Commands;

namespace Tests.IntegrationTests.AutofacMessageBus
{
    public static class AutofacMessagingRegistratorValues
    {
        public static readonly AutofacMessagingRegistrator CompleteAutofacMessagingRegistrator = new AutofacMessagingRegistrator(
            new List<Assembly>
            {
                typeof(StudentEvent).Assembly
            },
            new List<Assembly>()
            {
                typeof(AddNewStudentHandler).Assembly
            });
    }
}