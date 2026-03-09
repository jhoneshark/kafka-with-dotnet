using System.ComponentModel.DataAnnotations;

namespace DotNetWithKafka.Domain.DTOs;

public class UserDTO
{
    [Required(ErrorMessage = "Informe um cpf ou cnpj")]
    public string? CprOrCnpj { get; set; }
    
    [Required(ErrorMessage = "Informe um nome para o usuario")]
    [StringLength(100, MinimumLength = 2)]
    public string? Name { get; set; }
    
    [StringLength(150)]
    public string? Phone { get; set; }
    
    [Required(ErrorMessage = "Informe o email do usuario")]
    [StringLength(150, MinimumLength = 2)]
    [EmailAddress]
    public string? Email { get; set; }
    
    public int? RoleId { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}