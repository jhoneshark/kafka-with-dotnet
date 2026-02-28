using DotNetWithKafka.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetWithKafka.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        AddDatabaseServices(services);

        return services;
    }
    
    private static void AddDatabaseServices(IServiceCollection services)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
        var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var dbName = Environment.GetEnvironmentVariable("DB_DATABASE");

        string connectionString =
            $"Server={dbHost};Port={dbPort};User Id={dbUser};Password=\"{dbPassword}\";Database={dbName};Allow User Variables=true;";

        // Configuração do Entity Framework
        services.AddDbContextPool<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Configuração do Dapper
    }
}