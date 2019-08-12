using System.Threading.Tasks;
using Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Domain.BookId;
using static Domain.UserId;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public sealed class LendingController : CommandController
    {
        public LendingController(IConfiguration configuration) : base(configuration)
        {
        }
        
        [HttpPost]
        [Route(nameof(LendBook))]
        public Task<IActionResult> LendBook(
            string bookToLendId,
            string userId) => 
            SagaCommandExecutors(new LendBook(
                    BookIdFrom(bookToLendId),
                    UserIdFrom(userId)))
                .ToActionResult();

        [HttpPost]
        [Route(nameof(ReturnBook))]
        public Task<IActionResult> ReturnBook(
            string bookToReturnId,
            string borrowerUserId) =>
            SagaCommandExecutors(new ReturnBook(
                    BookIdFrom(bookToReturnId),
                    UserIdFrom(borrowerUserId)))
                .ToActionResult();
    }
}