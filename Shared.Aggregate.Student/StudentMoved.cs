using System;
using System.Collections.Generic;

namespace Aggregate.Student.Shared
{
    public sealed class StudentMoved : StudentEvent
    {
        public string City { get; }

        public StudentMoved(
            Guid aggregateRootId,
            string city) : base(aggregateRootId)
        {
            City = city;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return City;
        }
    }
}