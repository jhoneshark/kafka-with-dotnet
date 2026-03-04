namespace DotNetWithKafka.Domain.Entities;

public class Users
{
    public int Id { get; set; }
    public string? CprOrCnpj { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    
    public string? PasswordHash { get; set; }
    
    public int? RoleId { get; set; }
    public Roles Role { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExpiryTime { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}