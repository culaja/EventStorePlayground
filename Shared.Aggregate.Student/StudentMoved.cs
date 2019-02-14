using System;
using System.Collections.Generic;
using Common;

namespace Aggregate.Student.Shared
{
    public sealed class StudentMoved : StudentEvent
    {
        public Maybe<string> MaybeMovedFromCity { get; }
        public string MovedToCity { get; }

        public StudentMoved(
            Guid aggregateRootId,
            Maybe<string> maybeMovedFromCity,
            string movedToCity) : base(aggregateRootId)
        {
            MaybeMovedFromCity = maybeMovedFromCity;
            MovedToCity = movedToCity;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return MaybeMovedFromCity;
            yield return MovedToCity;
        }
    }
}