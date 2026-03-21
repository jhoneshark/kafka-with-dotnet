using DotNetWithKafka.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetWithKafka.Infrastructure.EntitiesConfiguration;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // Define a chave primária
        builder.HasKey(x => x.Id);

        // Configuração do Id para ser gerado automaticamente pelo banco (UUID/Guid) postgres
        // builder.Property(x => x.Id)
        //     .HasColumnName("id")
        //     .HasColumnType("uuid")
        //     .HasDefaultValueSql("gen_random_uuid()")
        //     .ValueGeneratedOnAdd()
        //     .IsRequired();

        // Configuração do CreatedAt no sql
        // builder.Property(x => x.CreatedAt)
        //     .HasColumnName("created_at")
        //     .HasColumnType("timestamp with time zone")
        //     .HasDefaultValueSql("now()")
        //     .ValueGeneratedOnAdd()
        //     .IsRequired();
        //
        // // Configuração do UpdatedAt
        // builder.Property(x => x.UpdatedAt)
        //     .HasColumnName("updated_at")
        //     .HasColumnType("timestamp with time zone")
        //     .HasDefaultValueSql("now()")
        //     .ValueGeneratedOnAdd()
        //     .IsRequired();
        
        //SQL
        builder.Property(t => t.CreatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.UpdatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate();
    }
}