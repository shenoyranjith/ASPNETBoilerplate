// See https://aka.ms/new-console-template for more information
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = CreateServices();

// Put the database update into a scope to ensure
// that all resources will be disposed.
using var scope = serviceProvider.CreateScope();
UpdateDatabase(scope.ServiceProvider, args);

/// <summary>
/// Configure the dependency injection services
/// </summary>
static IServiceProvider CreateServices()
{
    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Debug";
    Console.WriteLine("Running migrations with " + environmentName.Trim() + " configuration");
    var builder = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appSettings.{environmentName.Trim()}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();
    var configuration = builder.Build();

    var connectionString = configuration.GetConnectionString("DefaultConnection");

    return new ServiceCollection()
        // Add common FluentMigrator services
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            // Add SQLServer support to FluentMigrator
            .AddSqlServer()
            // Set the connection string
            .WithGlobalConnectionString(connectionString)
            // Define the assembly containing the migrations
            .ScanIn(typeof(Program).Assembly).For.Migrations())
        // Enable logging to console in the FluentMigrator way
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        // Build the service provider
        .BuildServiceProvider(false);
}

/// <summary>
/// Update the database
/// </summary>
static void UpdateDatabase(IServiceProvider serviceProvider, string[] args)
{
    bool isDown = false;
    try
    {
        isDown = bool.Parse(args[0]);
    }
    catch { }

    string? migrationVersion = null;
    long? version = null;
    try
    {
        migrationVersion = args[1];
        if (migrationVersion == "all")
        {
            version = 0;
        }
        else
        {
            version = long.Parse(args[1]);
        }
    }
    catch
    {
        if (isDown)
        {
            Console.Error.WriteLine("Migration version is needed for down.");
            Environment.Exit(1);
        }
    }

    // Instantiate the runner
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    // Execute the migrations
    if (isDown && version != null)
    {
        Console.WriteLine("Down migrating to " + version + "...");
        runner.RollbackToVersion(version.Value);
    }
    else
    {
        if (migrationVersion != "all" && version != null)
        {
            Console.WriteLine("Migrating to " + version);
            runner.MigrateUp(version.Value);
        }
        else
        {
            Console.WriteLine("Running all pending migrations...");
            runner.MigrateUp();
        }
    }
    Console.WriteLine("Completed running migrations");
}
