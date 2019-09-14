using System.Collections.Generic;

namespace LibraryEvents.UserEvents
{
    [ToString]
    public sealed class UserAdded : UserEvent
    {
        public string FullName { get; }

        public UserAdded(
            string aggregateId,
            string fullName) : base(aggregateId)
        {
            FullName = fullName;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return FullName;
        }
    }
}