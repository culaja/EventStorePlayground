using System;
using System.Threading.Tasks;
using Common;
using Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static WebApp.Api.ApplicationWrapping.WrappedUpCommandExecutors;

namespace WebApp.Api
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
            SagaCommandExecutors = SagaCommandExecutorsWith(configuration);
        }
    }
}