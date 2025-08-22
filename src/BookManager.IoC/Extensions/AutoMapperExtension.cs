using BookManager.Business.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.IoC.Extensions;
public static class AutoMapperExtension
{
    public static void AutoMapperConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(LoanProfile),
            typeof(UserProfile),
            typeof(UserProfile)
            );
    }
}
