namespace DotNetWithKafka.Domain.Entities;

public class Users : BaseEntity
{
    public string? CprOrCnpj { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    
    public string? PasswordHash { get; set; }
    
    public int? RoleId { get; set; }
    public Roles? Role { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExpiryTime { get; set; }
}