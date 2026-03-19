using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetWithKafka.Domain.Entities;

public abstract class BaseEntity
{
    [Column(Order = 0)]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}