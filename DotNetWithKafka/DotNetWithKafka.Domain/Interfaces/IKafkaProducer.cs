namespace DotNetWithKafka.Domain.Interfaces;

public interface IKafkaProducer
{
    Task ProducerAsync<T>(string topic, T message);
}