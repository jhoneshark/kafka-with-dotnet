namespace DotNetWithKafka.Domain.Interfaces;

public interface IKafkaHandler<T>
{
    /// <summary>
    /// Método responsável por processar a mensagem recebida do Kafka.
    /// </summary>
    /// <param name="message">O objeto deserializado do tópico.</param>
    Task HandleAsync(T message);
}