using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ParkManager.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        //todo add logging
        public void OnException(ExceptionContext context)
        {
            // Handle the exception and set the result
            context.Result = new ObjectResult($"An error occurred. Please use this code {context.HttpContext.TraceIdentifier} when contacting support")
            //log
            { 
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }


}
