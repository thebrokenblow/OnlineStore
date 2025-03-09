﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OnlineShop.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);

        return services;
    }
}