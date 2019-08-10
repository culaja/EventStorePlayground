using System.Collections.Generic;
using Common;

namespace Domain
{
    public sealed class BookName : ValueObject<BookName>
    {
        private readonly string _name;

        private BookName(string name)
        {
            _name = name;
        }
        
        public static BookName BookNameFrom(string name) => new BookName(name);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _name;
        }

        public override string ToString() => _name;

        public static implicit operator string(BookName bookName) => bookName.ToString();
    }
}