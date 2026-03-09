using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetWithKafka.Domain.Entities;

public abstract class BaseEntity
{
    [Column(Order = 0)]
    public int Id { get; set; }
    [Column(Order = 98)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column(Order = 99)]// Já inicializa com a data atual
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}