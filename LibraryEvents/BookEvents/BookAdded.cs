using System.Collections.Generic;

namespace LibraryEvents.BookEvents
{
    [ToString]
    public sealed class BookAdded : BookEvent
    {
        public string Name { get; }
        public int YearOfPrint { get; }

        public BookAdded(
            string id,
            string name,
            int yearOfPrint) : base(id)
        {
            Name = name;
            YearOfPrint = yearOfPrint;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return Name;
            yield return YearOfPrint;
        }
    }
}