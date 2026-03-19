namespace DotNetWithKafka.Domain.Entities;

public class UserDetails : BaseEntity
{
    public int UserId { get; set; }
    public Users User { get; set; }
    public DateOnly? Birthday { get; set; }
    public string? SocialName { get; set; }
    public string? NickName { get; set; }
}