using System.Text.Json;
using Confluent.Kafka;
using DotNetWithKafka.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetWithKafka.Infrastructure.KafkaMessaging;

public class KafkaConsumer<T> : BackgroundService
{
    private readonly IConsumer<string, string> _consumer;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _topic;

    public KafkaConsumer(string bootstrapServers, string topic, string groupId, IServiceProvider serviceProvider)
    {
        _topic = topic;
        _serviceProvider = serviceProvider;

        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        _consumer = new ConsumerBuilder<string, string>(config).Build();
    }
    
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        
        _consumer.Subscribe(_topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(stoppingToken);

                if (result?.Message != null)
                {
                    using var scope = _serviceProvider.CreateScope();

                    var handler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<T>>();

                    var message = JsonSerializer.Deserialize<T>(result.Message.Value);

                    if (message != null)
                    {
                        await handler.HandleAsync(message);
                    }
                }
            }
            catch (OperationCanceledException) { break; }
            catch (Exception ex)
            {
                // Se o Kafka estiver fora, ele apenas loga e continua tentando
                Console.WriteLine($"[Aguardando Brokers...] {ex.Message}");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
    
    public override void Dispose()
    {
        _consumer.Close();
        _consumer.Dispose();
        base.Dispose();
    }
}