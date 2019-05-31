using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using static System.Net.HttpStatusCode;

namespace WebApp.Controllers
{
    public static class TaskResultExtensions
    {
        public static async Task<IActionResult> ToActionResult(this Task<Result> resultTask)
        {
            var result = await resultTask;
            return result.IsSuccess
                ? (IActionResult) new ContentResult()
                {
                    StatusCode = (int) OK,
                    ContentType = "text/plain",
                    Content = ""
                }
                : BadRequestWith(result.Error);
        }

        private static JsonResult BadRequestWith(string error) => new JsonResult(error) {StatusCode = 400};
    }
}