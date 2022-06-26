using System.Net;
using System.Text.Json;
using ContactsApp.Core.Customs.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ContactsApp.Core.Middlewares
{
    public class UseExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleErrorFromMiddleware(context);
            }
        }

        private async Task HandleErrorFromMiddleware(HttpContext context)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.OK;
            var result = JsonSerializer.Serialize(new UnhandledException().HandleException());
            await response.WriteAsync(result);
            
        }
    }

}