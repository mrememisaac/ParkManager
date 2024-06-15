using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ParkManager.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Handle the exception and set the result
            context.Result = new ObjectResult("An error occurred")
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }


}
