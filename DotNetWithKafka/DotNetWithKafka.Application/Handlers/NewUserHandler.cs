using DotNetWithKafka.Domain.Entities;
using DotNetWithKafka.Domain.Interfaces;

namespace DotNetWithKafka.Application.Handlers;

public class NewUserHandler : IKafkaHandler<Users>
{
    public async Task HandleAsync(Users message)
    {
        // Lógica de teste:
        Console.WriteLine($"[CONSUMER Kafka Success] Mensagem recebida com sucesso!");
        Console.WriteLine($"Usuário: {message.Name} | Email: {message.Email}");

        // Como o método é Task, precisamos retornar algo ou usar await
        await Task.CompletedTask;
    }
}