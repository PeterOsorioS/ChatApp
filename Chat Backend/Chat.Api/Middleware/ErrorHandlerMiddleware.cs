using Chat.Excepciones.Excepcion;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Text.Json;

namespace Chat.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandlerExceptionAsync(context, error);
            }
        }
        private  Task HandlerExceptionAsync(HttpContext context, Exception error) 
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            var response = new Dictionary<string, string>
            {
                {"Error", error.Message}
            };
            switch (error)
            {
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
