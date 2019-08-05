using System.Collections.Generic;

namespace BallEvents
{
    public sealed class BallCreated : BallEvent
    {
        public string Name { get; }
        public int Size { get; }

        public BallCreated(string name, int size)
        {
            Name = name;
            Size = size;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in GetEqualityComponents()) yield return item;
            yield return Name;
            yield return Size;
        }
    }
}