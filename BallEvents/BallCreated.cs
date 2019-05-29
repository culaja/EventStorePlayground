using System.Collections.Generic;
using Common.Messaging;

namespace BallEvents
{
    public sealed class BallCreated : DomainEvent
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
            yield return Name;
            yield return Size;
        }
    }
}