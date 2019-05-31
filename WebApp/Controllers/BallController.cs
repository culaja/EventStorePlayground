using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static Domain.BallId;
using static WebApp.ApplicationWrapping.WrappedUpCommandExecutors;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BallController : ControllerBase
    {
        private Func<ICommand, Task<Result>> CommandExecutors { get; }

        public BallController(IConfiguration configuration)
        {
            CommandExecutors = DomainCommandExecutorsWith(configuration);
        }
        
        [HttpPost]
        [Route(nameof(Create))]
        public Task<IActionResult> Create(string ballId, int size) => 
            CommandExecutors(new CreateBall(BallIdFrom(ballId), size)).ToActionResult();

        [HttpPost]
        [Route(nameof(Pass))]
        public Task<IActionResult> Pass(string ballId, string destination) => 
            CommandExecutors(new PassBall(BallIdFrom(ballId), destination)).ToActionResult();
    }
}