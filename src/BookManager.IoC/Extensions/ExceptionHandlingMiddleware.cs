using BookManager.Domain.Commom.Enums;
using BookManager.Domain.Commom.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace BookManager.IoC.Extensions;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Create a Result failure object with the exception message
            var error = new Error(Issues.e500, "An unexpected error occurred: " + ex.Message);
            var result = Result.Failure(error);

            var json = JsonConvert.SerializeObject(result);
            await context.Response.WriteAsync(json);
        }
    }
}
