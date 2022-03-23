using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares
{
    /// <summary>
    /// Handle errors globally
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Handle errors
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case BaseException e:
                        // custom application error
                        response.StatusCode = (int)e.StatusCode;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                
                await response.WriteAsync(new ErrorDetails
                {
                    Message = error?.Message,
                    StatusCode = response.StatusCode
                }.ToString());
            }
        }
    }
}