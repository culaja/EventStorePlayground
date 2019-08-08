using static Domain.Book.BookId;
using static Domain.Book.BookName;
using static Domain.Book.YearOfPrint;

namespace Domain.Book
{
    public static class ToDomainObjectExtensions
    {
        public static BookId ToBookId(this string bookIdString) =>
            BookIdFrom(bookIdString);
        
        public static BookName ToBookName(this string bookNameString) =>
            BookNameFrom(bookNameString);
        
        public static YearOfPrint ToYearOfPrint(this int yearOfPrintInt) =>
            YearOfPrintFrom(yearOfPrintInt);
    }
}