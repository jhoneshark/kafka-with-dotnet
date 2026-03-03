namespace DotNetWithKafka.Domain.Entities;

public class Roles
{
    public int Id { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Users> Users { get; set; } = new List<Users>();
}