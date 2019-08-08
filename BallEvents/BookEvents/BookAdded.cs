using System.Collections.Generic;

namespace BallEvents.BookEvents
{
    public sealed class BookAdded : BookEvent
    {
        public string Name { get; }
        public int DateOfPrint { get; }

        public BookAdded(
            string name,
            int dateOfPrint)
        {
            Name = name;
            DateOfPrint = dateOfPrint;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in GetEqualityComponents()) yield return item;
            yield return Name;
            yield return DateOfPrint;
        }
    }
}