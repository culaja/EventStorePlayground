using Common.Commanding;
using Domain.StudentDomain;

namespace DomainServicesWrapup
{
    public static class ServicesProxy
    {
        public static ICommandBus<Student> StudentCommandBus { get; }
    }
}