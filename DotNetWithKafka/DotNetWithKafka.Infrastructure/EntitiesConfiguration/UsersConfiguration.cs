using DotNetWithKafka.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetWithKafka.Infrastructure.EntitiesConfiguration;

public class UsersConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.CprOrCnpj).HasMaxLength(14).IsRequired();
        builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
        builder.Property(t => t.Phone).HasMaxLength(25);
        builder.Property(p => p.Email).HasMaxLength(150).IsRequired();
        builder.Property(t => t.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(t => t.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(t => t.CprOrCnpj).IsUnique();
        
        // Configuração do Relacionamento (1 Role para N Users)
        builder.HasOne(u => u.Role)          // Um usuário tem um cargo
            .WithMany()                           // (Opcional) se a classe Roles não tiver ICollection<Users>
            .HasForeignKey(u => u.RoleId)  // A chave estrangeira é RoleId
            .OnDelete(DeleteBehavior.Restrict); // Evita deletar um Role que tenha usuários vinculados
    }
}