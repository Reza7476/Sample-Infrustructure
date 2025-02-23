using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsetting.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        EnsureDatabaseExists(connectionString);

        var serviceProvider = CreateServices(connectionString);

        using (var scope = serviceProvider.CreateScope())
        {
            UpdateDatabase(scope.ServiceProvider);
        }
    }
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        Console.WriteLine("Starting database migrations...");
        runner.MigrateUp();
        Console.WriteLine("Database migrations completed!");
    }


    private static IServiceProvider CreateServices(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }


    private static void EnsureDatabaseExists(string connectionString)
    {
        var databaseName = GetDatabaseName(connectionString);
        var masterConnectionString = ChangeDatabaseName(connectionString, "master");

        using var connection = new SqlConnection(masterConnectionString);
        connection.Open();

        var commandText = $"IF DB_ID(N'{databaseName}') IS NULL CREATE DATABASE [{databaseName}]";
        using var command = new SqlCommand(commandText, connection);
        command.ExecuteNonQuery();

        connection.Close();
    }

    private static string GetDatabaseName(string connectionString)
    {
        return new SqlConnectionStringBuilder(connectionString).InitialCatalog;
    }

    private static string ChangeDatabaseName(string connectionString, string databaseName)
    {
        var builder = new SqlConnectionStringBuilder(connectionString)
        {
            InitialCatalog = databaseName
        };
        return builder.ConnectionString;
    }
}
