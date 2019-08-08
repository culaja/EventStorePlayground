using LibraryEvents.BookEvents;
using Common;

namespace Domain.Book
{
    public sealed class Book : AggregateRoot
    {
        private YearOfPrint _yearOfPrint;
        private BookName _name;

        public static Book NewBookFrom(BookId id, BookName bookName, YearOfPrint yearOfPrint) => 
            (Book)new Book().ApplyChange(new BookAdded(id, bookName, yearOfPrint));

        private void Apply(BookAdded e)
        {
            SetAggregateId(e.Id.ToBookId());
            _name = e.Name.ToBookName();
            _yearOfPrint = e.YearOfPrint.ToYearOfPrint();
        }
        
        public override string ToString() => $"{Id}: {_yearOfPrint}";
    }
}