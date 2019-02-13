using System;

namespace Aggregate.Student.Shared
{
    public interface IDomainEventDto
    {
        Guid AggregateRootId { get; set; }
        ulong Version { get; set; }
    }
}