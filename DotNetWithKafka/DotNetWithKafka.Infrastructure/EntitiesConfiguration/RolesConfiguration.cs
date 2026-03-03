using DotNetWithKafka.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetWithKafka.Infrastructure.EntitiesConfiguration;

public class RolesConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(p => p.Description).HasMaxLength(50).IsRequired();

        builder.HasData(
            new Roles 
            { 
                Id = 1, 
                Description = "Users" 
            },
            new Roles 
            { 
                Id = 2, 
                Description = "Admin" 
            },
            new Roles 
            { 
                Id = 2, 
                Description = "Admin ROOT" 
            }
        );
    }
}