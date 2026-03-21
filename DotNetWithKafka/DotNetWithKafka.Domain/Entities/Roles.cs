namespace DotNetWithKafka.Domain.Entities;

public class Roles : BaseEntity
{
    public string? Description { get; set; }
    
    public ICollection<Users> Users { get; set; } = new List<Users>();
}