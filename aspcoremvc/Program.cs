using System;
using System.IO;
using Data;
using Business;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Core;
using Microsoft.AspNetCore.Http;
using Common;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog.Sinks.MSSqlServer;

namespace Web
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true)
            .Build();

        public static void Main(string[] args)
        {
            Log.Logger = SeriConfigure(Configuration);

            try
            {
                Log.Information("Getting the motors running...");

                var host = BuildWebHost(args);

                using (var scope = host.Services.CreateScope())
                {
                    MapperHelper.InitMaps(scope.ServiceProvider);
                    SeedData.Initialize(scope.ServiceProvider.GetService<AppDbContext>(), scope.ServiceProvider);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
        public static Serilog.ILogger SeriConfigure(IConfiguration Configuration, bool enabledb = true)
        {
            var opts = new ColumnOptions();
            //opts.Store.Remove(StandardColumn.Properties);
            //opts.Store.Remove(StandardColumn.MessageTemplate);
            opts.Store.Add(StandardColumn.LogEvent);
            HttpHelper.ConnStr = Configuration.GetConnectionString(Configuration["AppConfig:ActiveConnection"]);
            var conf = new LoggerConfiguration().Enrich.FromLogContext().Enrich.With<HttpContextEnricher>();
            if (bool.Parse(Configuration["Serilog:EnableFileLogging"]))
                conf = conf.WriteTo.File(Configuration["Serilog:FileName"], rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Day);
            if (bool.Parse(Configuration["Serilog:LogOnlyErrors"]))
                conf = conf.MinimumLevel.Override("Microsoft", LogEventLevel.Error).MinimumLevel.Override("System", LogEventLevel.Error).MinimumLevel.Override("Hangfire", LogEventLevel.Error);

            if (enabledb && bool.Parse(Configuration["Serilog:EnableDBLogging"]) && !bool.Parse(Configuration["AppConfig:UseInMemoryDB"]))
                conf.WriteTo.MSSqlServer(HttpHelper.ConnStr, Configuration["Serilog:TableName"], autoCreateSqlTable: true, columnOptions: opts);

            return conf.CreateLogger();
        }
    }
    public class HttpContextEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var accessor = HttpHelper.GetService<IHttpContextAccessor>();
            if (accessor != null)
            {
                var prop = propertyFactory.CreateProperty("FullUrl", accessor.HttpContext.Request.GetDisplayUrl());
                logEvent.AddPropertyIfAbsent(prop);
            }

        }
    }
}