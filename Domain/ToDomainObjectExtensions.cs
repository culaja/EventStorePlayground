namespace Domain
{
    public static class ToDomainObjectExtensions
    {
        public static BookId ToBookId(this string bookIdString) =>
            BookId.BookIdFrom(bookIdString);
        
        public static BookName ToBookName(this string bookNameString) =>
            BookName.BookNameFrom(bookNameString);
        
        public static YearOfPrint ToYearOfPrint(this int yearOfPrintInt) =>
            YearOfPrint.YearOfPrintFrom(yearOfPrintInt);
    }
}