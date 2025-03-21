using BookManager.Domain.Extensions;
using BookManager.Infra.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.IoC.Extensions;
public static class HealthChecksExtension
{
    public static IServiceCollection AddHealthChecksConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddDbContextCheck<BookManagerDbContext>();

        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(5);
            opt.MaximumHistoryEntriesPerEndpoint(10);
            opt.AddHealthCheckEndpoint("BookManager.Api", "/health");
        })
            .AddSqlServerStorage(configuration.GetConnectionString(ConstantsConf.BookConnection));

        return services;
    }

    public static IApplicationBuilder UseHealthChecksConfig(this IApplicationBuilder app)
    {
        // Generate an endpoint that will return data to be used on dashboard
        app.UseHealthChecks("/health", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        // Active the dashboard to view the status of each health check
        app.UseHealthChecksUI(opt => { opt.UIPath = "/dashboard"; });

        return app;
    }

}
