using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Domain.Commands;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static WebApp.ApplicationWrapping.WrappedUpCommandExecutors;

namespace WebApp.Controllers
{
    public abstract class CommandController : ControllerBase
    {
        protected Func<BookCommand, Task<Result>> BookCommandExecutors { get; }
        
        protected Func<UserCommand, Task<Result>> UserCommandExecutors { get; }
        
        protected Func<SagaCommand, Task<Result>> SagaCommandExecutors { get; }

        protected CommandController(IConfiguration configuration)
        {
            BookCommandExecutors = BookCommandExecutorsWith(configuration);
            UserCommandExecutors = UserCommandExecutorsWith(configuration);
        }
    }
}