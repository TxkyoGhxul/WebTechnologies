using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Infrastructure.Data;
using WebTechnologies.Infrastructure.Repositories;

namespace WebTechnologies.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUserDbContext, UserDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IUnitOfWork, UserDbContext>();

        return services;
    }

    public static IServiceCollection InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
