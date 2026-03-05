using DotNetWithKafka.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotNetWithKafka.Api.Controllers; // Verifique se o namespace está correto

[ApiController] // Essencial para o Swagger reconhecer a API
[Route("api/[controller]")] // Define a rota base, ex: api/my
public class MyController : ControllerBase
{
    private readonly IKafkaProducer _producer;
    
    public MyController(IKafkaProducer producer)
    {
        _producer = producer;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage()
    {
        await _producer.ProducerAsync("meu-topico-test", new { Id = 1, Nome = "DotNet + Kafka" });
        return Ok("Mensagem enviada com sucesso!");
    }
}