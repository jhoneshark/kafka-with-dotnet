using Asp.Versioning;
using DotNetWithKafka.Application.Handlers;
using DotNetWithKafka.Domain.Entities;
using DotNetWithKafka.Domain.Interfaces;
using DotNetWithKafka.Infrastructure.Config;
using DotNetWithKafka.Infrastructure.KafkaMessaging;
using DotNetWithKafka.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetWithKafka.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        AddApplicationServices(services);
        AddDatabaseServices(services);
        AddDependencyInjection(services);
        AddApiVersioningServices(services);
        AddPolicyCors(services);
        AddKafkaExtensions(services);

        return services;
    }

    private static void AddApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UserRepository>();
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

    private static void AddDependencyInjection(IServiceCollection services)
    {
        services.AddSingleton<IKafkaProducer, KafkaProducer>();
    }

    private static void AddApiVersioningServices(IServiceCollection services)
    {
        // Exemplo de acesso conforme a configuração:
        // 2. Via Query String: http://localhost:5041/api/version2?api-version=2.0
        // http://localhost:5193/Users?api-version=1(Parâmetro na query)
        
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;

            // Define onde o sistema deve procurar a versão. O 'Combine' permite que a API aceite
            // a versão na Query String.
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version")
            );
        }).AddApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'V";
            opt.SubstituteApiVersionInUrl = true;
        });
    }
    
    private static void AddPolicyCors(IServiceCollection services)
    {
        services.AddCors(options => 
        {
            options.AddDefaultPolicy(builder => 
            {
                builder.AllowAnyOrigin()   // Permite qualquer IP/Domínio
                    .AllowAnyMethod()   // Permite GET, POST, PUT, DELETE, OPTIONS
                    .AllowAnyHeader();  // Permite Authorization, X-Requested-With, etc
            });
        });
    }

    private static void AddKafkaExtensions(IServiceCollection services)
    {
        string bootstrapServers = "localhost:9092,localhost:9093,localhost:9094";
        
        services.AddScoped<IKafkaHandler<Users>, NewUserHandler>();
        services.AddHostedService(sp => 
            new KafkaConsumer<Users>(bootstrapServers, "novo-user", "user-group", sp));
        
        // --- CONSUMIDOR DE LOGS (Outro Modelo e Outro Tópico) --- Exemplo  
        // string bootstrapServers = "localhost:9092"; apenas 1 brocker
        // services.AddScoped<IKafkaHandler<LogSystem>, LogSystemHandler>();
        // services.AddHostedService(sp => 
        //     new KafkaConsumer<LogSystem>("system-logs", "log-group", sp));
    }
}