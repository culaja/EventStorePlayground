using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutofacMessageBus;
using Domain.StudentDomain;
using DomainServices;
using Module = Autofac.Module;

namespace AutofacApplicationWrapUp
{
    public sealed class MainRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<MessagingRegistrator>();
        }
    }
    
    public class MessagingRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacMessagingRegistrator(
                new List<Assembly> {typeof(Student).Assembly},
                new List<Assembly>() {typeof(StudentMovedHandler).Assembly}));
        }
    }
}