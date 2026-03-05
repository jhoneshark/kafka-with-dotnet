using System.Text.Json;
using Confluent.Kafka;
using DotNetWithKafka.Domain.Interfaces;

namespace DotNetWithKafka.Infrastructure.KafkaMessaging;

public class KafkaProducer : IKafkaProducer, IDisposable
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducer()
    {
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092,localhost:9093,localhost:9094",
            Acks = Acks.All,
        };

        _producer = new ProducerBuilder<string, string>(config).Build();
    }
    
    public async Task ProducerAsync<T>(string topic, T message)
    {
        var json = JsonSerializer.Serialize(message);

        var kafkaMessage = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = json
        };
        
        await _producer.ProduceAsync(topic, kafkaMessage);
    }

    public void Dispose()
    {
        _producer?.Flush();
        _producer?.Dispose();
    }
}