using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Extensions.Hosting;
using Serilog.Formatting;
using Serilog.Formatting.Json;
using Test.Work.BooksCatalogApi.Database;
using Test.Work.BooksCatalogApi.Extensions;
using Test.Work.BooksCatalogApi.Options;

namespace Test.Work.BooksCatalogApi;

public static class Program
{
    private static readonly ITextFormatter ConsoleTextFormatter = new JsonFormatter();

    public static async Task<int> Main(string[]? args)
    {
        Log.Logger = CreateBootstrapLogger();
        IHostEnvironment? hostEnvironment = null;

        try
        {
            Log.Information("Initialising.");
            var host = CreateHostBuilder(args).Build();
            hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
            hostEnvironment.ApplicationName = AssemblyInformation.Current.Product;

            Log.Information(
                "Running {Application} migrations in {Environment} mode.",
                hostEnvironment.ApplicationName,
                hostEnvironment.EnvironmentName);

            using (var services = host.Services.CreateScope())
            {
                var migrator = services.ServiceProvider.GetRequiredService<DbMigrator>();
                await migrator.MigrateAsync();
            }
            Log.Information(
                "Started {Application} in {Environment} mode.",
                hostEnvironment.ApplicationName,
                hostEnvironment.EnvironmentName);

            await host.RunAsync().ConfigureAwait(false);

            Log.Information(
                "Stopped {Application} in {Environment} mode.",
                hostEnvironment.ApplicationName,
                hostEnvironment.EnvironmentName);
            return 0;
        }
        catch (Exception exception)
        {
            if (hostEnvironment is null)
                Log.Fatal(exception, "Application terminated unexpectedly while initialising.");
            else
                Log.Fatal(
                    exception,
                    "{Application} terminated unexpectedly in {Environment} mode.",
                    hostEnvironment.ApplicationName,
                    hostEnvironment.EnvironmentName);

            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[]? args) =>
        new HostBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureHostConfiguration(
                configurationBuilder => configurationBuilder
                    .AddEnvironmentVariables(prefix: "DOTNET_")
                    .AddIf(
                        args is not null,
                        x => x.AddCommandLine(args!)))
            .ConfigureAppConfiguration((hostingContext, config) =>
                AddConfiguration(config, hostingContext.HostingEnvironment, args))
            .UseSerilog(ConfigureApplicationLogger)
            .UseDefaultServiceProvider(
                (context, options) =>
                {
                    var isDevelopment = context.HostingEnvironment.IsDevelopment();
                    options.ValidateScopes = isDevelopment;
                    options.ValidateOnBuild = isDevelopment;
                })
            .ConfigureWebHost(ConfigureWebHostBuilder)
            .UseConsoleLifetime();

    private static void ConfigureWebHostBuilder(IWebHostBuilder webHostBuilder) =>
        webHostBuilder
            .UseKestrel(
                (builderContext, options) =>
                {
                    options.AddServerHeader = false;
                    options.Configure(builderContext.Configuration.GetSection(nameof(GlobalOptions.Kestrel)), reloadOnChange: false);
                })
            .UseIIS()
            .UseStartup<Startup>();

    private static IConfigurationBuilder AddConfiguration(
        IConfigurationBuilder configurationBuilder,
        IHostEnvironment hostEnvironment,
        string[]? args) =>
        configurationBuilder
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: false)
            .AddKeyPerFile(Path.Combine(Directory.GetCurrentDirectory(), "configuration"), optional: true, reloadOnChange: false)
            .AddIf(
                hostEnvironment.IsDevelopment() && !string.IsNullOrEmpty(hostEnvironment.ApplicationName),
                x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true, reloadOnChange: false))
            .AddEnvironmentVariables()
            .AddIf(
                args is not null,
                x => x.AddCommandLine(args!));

    private static ReloadableLogger CreateBootstrapLogger()
    {
        var isDevelopment = string.Equals(
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            "development",
            StringComparison.OrdinalIgnoreCase);

        var configuration = new LoggerConfiguration()
            .WriteTo.Debug()
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }));

        configuration = isDevelopment
            ? configuration.WriteTo.Console()
            : configuration.WriteTo.Console(ConsoleTextFormatter);

        return configuration.CreateBootstrapLogger();
    }

    private static void ConfigureApplicationLogger(
        HostBuilderContext context,
        IServiceProvider services,
        LoggerConfiguration configuration)
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }));

        if (context.HostingEnvironment.IsDevelopment())
            configuration.WriteTo.Console();
        else
            configuration.WriteTo.Console(ConsoleTextFormatter);
    }
}