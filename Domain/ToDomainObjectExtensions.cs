using static Domain.UserId;

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
        
        public static UserId ToUserId(this string userIdString) =>
            UserIdFrom(userIdString);
        
        public static FullName ToFullName(this string fullNameString) =>
            FullName.FullNameFrom(fullNameString);
    }
}