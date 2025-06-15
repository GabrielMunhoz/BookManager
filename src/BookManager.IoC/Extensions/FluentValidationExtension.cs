using BookManager.Domain.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.IoC.Extensions;
public static class FluentValidationExtension
{
    public static IServiceCollection ConfigFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UserValidator>();
        services.AddValidatorsFromAssemblyContaining<LoanValidator>();

        return services;
    }
}
