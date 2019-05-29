using System.Collections.Generic;
using Common.Messaging;

namespace BallEvents
{
    public sealed class BallPassed : DomainEvent
    {
        public string Name { get; }
        public string PassFrom { get; }
        public string PassTo { get; }

        public BallPassed(string name, string passFrom, string passTo)
        {
            Name = name;
            PassFrom = passFrom;
            PassTo = passTo;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return PassFrom;
            yield return PassTo;
        }
    }
}