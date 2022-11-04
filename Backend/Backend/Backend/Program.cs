using Backend.PL.Extensions;
using Backend.PL.Settings.AppSettings;
using log4net;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.RollingFileAlternative;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog();
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddLog4Net();

        Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .WriteTo.Logger(l =>
                            l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information).WriteTo
                                .RollingFile(@"Logs\Info-{Date}.log"))
                        .WriteTo.Logger(l =>
                            l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug).WriteTo
                                .RollingFile(@"Logs\Debug-{Date}.log"))
                        .WriteTo.Logger(l =>
                            l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning).WriteTo
                                .RollingFile(@"Logs\Warning-{Date}.log"))
                        .WriteTo.Logger(l =>
                            l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error).WriteTo
                                .RollingFile(@"Logs\Error-{Date}.log"))
                        .WriteTo.Logger(l =>
                            l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal).WriteTo
                                .RollingFile(@"Logs\Fatal-{Date}.log"))
                        .WriteTo.RollingFile(@"Logs\Verbose-{Date}.log")
                        .CreateLogger();

        var appSettings = RegisterSettings(builder.Configuration);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.InjectDependencies();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.RegistryDatabase(appSettings);
        builder.Services.RegisterAutoMapper();


        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                //.AllowCredentials();
            });
        });


        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseCors("AllowAll");
        app.MapControllers();
        app.RegisterExceptionHandler(Log.Logger);

        app.UseEndpoints((endpoints) =>
        {
            endpoints.MapGet("/", async context => await context.Response.WriteAsync("healthy. open swagger route /swagger"));
        });
        app.Run();


        static AppSettings RegisterSettings(IConfiguration configuration) =>
            new()
            {
                Database = configuration.GetSection(nameof(AppSettings.Database)).Get<DatabaseSettings>(),
            };
    }
}