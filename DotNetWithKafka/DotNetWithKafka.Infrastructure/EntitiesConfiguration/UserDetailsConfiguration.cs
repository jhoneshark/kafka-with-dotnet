using DotNetWithKafka.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetWithKafka.Infrastructure.EntitiesConfiguration;

public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnOrder(0);
        builder.Property(t => t.UserId).HasColumnOrder(1);
        builder.Property(t => t.Birthday).HasColumnOrder(2);
        builder.Property(t => t.SocialName)
            .HasMaxLength(100)
            .IsRequired()
            .HasColumnOrder(3);
        builder.Property(t => t.NickName)
            .HasMaxLength(50)
            .HasColumnOrder(4);
        builder.Property(t => t.CreatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd()
            .HasColumnOrder(98);

        builder.Property(t => t.UpdatedAt)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAddOrUpdate()
            .HasColumnOrder(99);
    }
}