using System;
using System.Text.Json.Serialization;
using DebuggingLearning.Tasks.Extensions;
using DebuggingLearning.WebApi.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

static WebApplicationBuilder CreateWebBuilder(string[] args)
{
    var builder = WebApplication.CreateBuilder(args);

    var config = new ConfigurationBuilder()
       .SetBasePath(Environment.CurrentDirectory)
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
       .Build();

    LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

    // Add services to the container.
    builder.Services
        .AddControllers()
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    builder.Services
        .AddSwaggerGen()
        .AddServices()
        .AddTasks();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    return builder;
}

var logger = LogManager.GetCurrentClassLogger();
try
{
    var builder = CreateWebBuilder(args);

    logger.Info("Start application...");
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // Routing...
    app.MapControllers();

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    logger.Info("End application");
    LogManager.Shutdown();
}
