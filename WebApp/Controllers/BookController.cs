using System.Threading.Tasks;
using Domain.Book.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Domain.Book.BookId;
using static Domain.Book.YearOfPrint;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public sealed class BookController : CommandController
    {
        public BookController(IConfiguration configuration) : base(configuration)
        {
        }
        
        [HttpPost]
        [Route(nameof(Add))]
        public Task<IActionResult> Add(string name, int yearOfPrint) => 
            CommandExecutors(new AddBook(
                BookIdFrom(name), 
                YearOfPrintFrom(yearOfPrint)))
                .ToActionResult();
    }
}