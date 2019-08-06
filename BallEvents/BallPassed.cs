using System.Collections.Generic;

namespace BallEvents
{
    public sealed class BallPassed : BallEvent
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
            foreach (var item in GetEqualityComponents()) yield return item;
            yield return Name;
            yield return PassFrom;
            yield return PassTo;
        }
        
        public override string ToString() => $"{nameof(BallPassed)}, {Name}, {PassFrom}, {PassTo}";
    }
}