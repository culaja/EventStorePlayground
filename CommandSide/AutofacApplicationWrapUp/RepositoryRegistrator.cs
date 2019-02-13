using Autofac;
using InMemory;
using Ports.Repositories;

namespace AutofacApplicationWrapUp
{
    public sealed class RepositoryRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StudentInMemoryRepository>().As<IStudentRepository>().SingleInstance();
        }
    }
}