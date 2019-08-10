using System.Threading.Tasks;
using Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Domain.BookId;
using static Domain.BookName;
using static Domain.YearOfPrint;

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
        public Task<IActionResult> Add(
            string id,
            string name,
            int yearOfPrint) => 
            CommandExecutors(new AddBook(
                BookIdFrom(id), 
                BookNameFrom(name), 
                YearOfPrintFrom(yearOfPrint)))
                .ToActionResult();
    }
}