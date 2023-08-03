using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IntegrationTest;

public class Testing
{

    private static WebApplicationFactory<Program> _factory = null!;
    // private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    public Testing()
    {
        var services = new ServiceCollection();
        services.AddLogging();

        _factory = new CustomWebApplicationFactory();
        // _configuration = _factory.Services.GetRequiredService<IConfiguration>();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
   where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<BlogDataContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<BlogDataContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<BlogDataContext>();

        return await context.Set<TEntity>().CountAsync();
    }
}