using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebTechnologies.Application.Authentication;
using WebTechnologies.Application.Behaviors;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Application.Sorters;
using WebTechnologies.Application.Sorters.Base;
using WebTechnologies.Application.Sorters.Fields;
using WebTechnologies.Domain.Models;

namespace WebTechnologies.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection InjectMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerfomanceBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));

        return services;
    }

    public static IServiceCollection InjectValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection InjectSorters(this IServiceCollection services)
    {
        services.AddScoped<ISorter<User, UserSortField>, UserSorter>();

        return services;
    }

    public static IServiceCollection InjectAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection InjectJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

        return services;
    }
}
