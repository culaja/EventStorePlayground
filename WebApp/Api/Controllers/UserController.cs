using System.Threading.Tasks;
using Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Domain.FullName;
using static Domain.UserId;

namespace WebApp.Api.Controllers
{
    [Route("api/[controller]")]
    public sealed class UserController : CommandController
    {
        public UserController(IConfiguration configuration) : base(configuration)
        {
        }
        
        [HttpPost]
        [Route(nameof(Add))]
        public Task<IActionResult> Add(
            string id,
            string fullName) => 
            UserCommandExecutors(new AddUser(
                    UserIdFrom(id), 
                    FullNameFrom(fullName)))
                .ToActionResult();
    }
}