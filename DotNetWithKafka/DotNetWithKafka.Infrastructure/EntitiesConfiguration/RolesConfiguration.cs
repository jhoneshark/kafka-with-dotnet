using DotNetWithKafka.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetWithKafka.Infrastructure.EntitiesConfiguration;

public class RolesConfiguration : BaseEntityConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
    {
        base.Configure(builder);
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
                Id = 3, 
                Description = "Admin ROOT" 
            }
        );
    }
}