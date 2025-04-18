﻿using BookManager.Business.Services;
using BookManager.Domain.Interface.Repositories;
using BookManager.Domain.Interface.Services;
using BookManager.Infra.Respository;
using Microsoft.Extensions.DependencyInjection;

namespace BookManager.IoC.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection ConfigServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoanService, LoanService>();

        return services; 
    }
    
    public static IServiceCollection ConfigRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        return services; 
    }
}
