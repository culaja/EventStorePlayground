using System;
using System.Threading.Tasks;
using Common;
using Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApp.ApplicationWrapping;

namespace WebApp.Controllers
{
    public abstract class CommandController : ControllerBase
    {
        protected Func<ICommand, Task<Result>> CommandExecutors { get; }

        protected CommandController(IConfiguration configuration)
        {
            CommandExecutors = WrappedUpCommandExecutors.DomainCommandExecutorsWith(configuration);
        }
    }
}