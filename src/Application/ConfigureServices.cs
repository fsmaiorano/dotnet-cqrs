﻿using Application.Common.Behaviours;
using Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            // cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        if (AppDomain.CurrentDomain.FriendlyName.Contains("testhost"))
        {
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log-.txt");
            Log.Logger = new LoggerConfiguration()
                               .MinimumLevel.Information()
                               .WriteTo.Console()
                               .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                               .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }
        else
        {
            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "log-.txt");
            Log.Logger = new LoggerConfiguration()
                             .MinimumLevel.Error()
                             .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                             .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }

        return services;
    }
}