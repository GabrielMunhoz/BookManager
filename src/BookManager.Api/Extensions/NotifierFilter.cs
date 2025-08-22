using BookManager.Domain.Commom.Results;
using BookManager.Domain.Interface.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace BookManager.Api.Extensions;
//This filter is responsible for return 404 on action when there are errors in notifier.
public class NotifierFilter : IAsyncResultFilter
{
    private readonly INotifier _notifier;
    private readonly ILogger<NotifierFilter> _logger;

    public NotifierFilter(INotifier Notifier, ILogger<NotifierFilter> logger)
    {
        _notifier = Notifier;
        _logger = logger;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notifier.HasErrors)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var errors = _notifier.Errors
                .Select(n => new Error(n.Issue, n.Message))
                .ToList();

            var result = Result.Failure(errors);

            var json = JsonConvert.SerializeObject(result);

            _logger.LogError(json);
            
            await context.HttpContext.Response.WriteAsync(json);

            return;
        }

        await next();
    }
}
