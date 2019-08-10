using System;
using System.Linq.Expressions;
using Common.Messaging;

namespace UnitTests
{
    public static class AssertionsHelpers
    {
        public static Expression<Func<IDomainEvent, bool>> EventOf<T>() =>
            e => e.GetType() == typeof(T);
    }
}