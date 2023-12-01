using Newtonsoft.Json;
using System.Net;
using Tuya.PruebaTecnica.WebApi.Exceptions;

namespace Tuya.PruebaTecnica.WebApi.Midlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; 
            if (exception is NotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;            

            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
