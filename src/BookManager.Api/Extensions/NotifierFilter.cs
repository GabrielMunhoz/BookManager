using BookManager.Domain.Commom.Results;
using BookManager.Domain.Interface.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace BookManager.Api.Extensions;

public class NotifierFilter : IAsyncResultFilter
{
    private readonly INotifier _notifier;

    public NotifierFilter(INotifier Notifier)
    {
        _notifier = Notifier;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notifier.HasErrors)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var errors = _notifier.Errors
                .Select(n => new Error($"{n.Key}: {n.Message}"))
                .ToList();

            var result = Result.Failure(errors);

            var json = JsonConvert.SerializeObject(result);
            await context.HttpContext.Response.WriteAsync(json);

            return;
        }

        await next();
    }
}
