using Application.Common.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(provider => configuration);
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<BlogDataContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
              builder => builder.MigrationsAssembly(typeof(BlogDataContext).Assembly.FullName)));

        services.AddScoped<IBlogDataContext>(provider => provider.GetRequiredService<BlogDataContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}
