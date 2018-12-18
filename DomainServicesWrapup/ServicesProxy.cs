using System;
using Common.Commanding;

namespace DomainServicesWrapup
{
    public static class ServicesProxy
    {
        public static ICommandBus CommandBus { get; }
    }
}