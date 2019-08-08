using LibraryEvents.BookEvents;
using Common;
using static Domain.Book.BookId;

namespace Domain.Book
{
    public sealed class Book : AggregateRoot
    {
        private YearOfPrint _yearOfPrint;
        
        public static Book NewBookFrom(BookId id, YearOfPrint yearOfPrint) => 
            (Book)new Book().ApplyChange(new BookAdded(id, yearOfPrint));

        private void Apply(BookAdded e)
        {
            SetAggregateId(BookIdFrom(e.Name));
            _yearOfPrint = YearOfPrint.YearOfPrintFrom(e.DateOfPrint);
        }
        
        public override string ToString() => $"{Id}: {_yearOfPrint}";
    }
}