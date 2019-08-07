using System.Collections.Generic;
using Common;

namespace Domain.Book
{
    public sealed class BookId : AggregateId
    {
        private readonly string _name;

        private BookId(string name)
        {
            _name = name;
        }
        
        public static BookId BookIdFrom(string name) => new BookId(name);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }

        public override string ToString() => _name;

        public static implicit operator string(BookId bookId) => bookId.ToString();
    }
}