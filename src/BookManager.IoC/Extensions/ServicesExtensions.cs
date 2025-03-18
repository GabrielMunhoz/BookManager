using Microsoft.Extensions.DependencyInjection;

namespace BookManager.IoC.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection ConfigServices(this IServiceCollection services)
    {

        return services; 
    }
}
