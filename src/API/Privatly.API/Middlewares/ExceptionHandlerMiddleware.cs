using System.Net;
using Newtonsoft.Json;

namespace Privatly.API.Middlewares;

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static async Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = exception switch
            {
                not null => (int) HttpStatusCode.InternalServerError,
                _ => response.StatusCode
            };

            var result = JsonConvert.SerializeObject(new
            {
                ErrorMessage = "Oops, internal server error happens... :("
            });

            await response.WriteAsync(result);
        }
    }