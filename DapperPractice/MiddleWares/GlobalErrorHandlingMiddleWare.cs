using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DapperPractice.MiddleWares
{
    public sealed class GlobalErrorHandlingMiddleWare : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlingMiddleWare> _logger;

        public GlobalErrorHandlingMiddleWare(ILogger<GlobalErrorHandlingMiddleWare> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //HttpsStatusCode => Enum
                await context.Response.WriteAsJsonAsync(new ErrorDetails
                { 
                    StatusCode= (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }
    }
}
