using DotNetWithKafka.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetWithKafka.Infrastructure.EntitiesConfiguration;

public class UserDetailsConfiguration : BaseEntityConfiguration<UserDetails>
{
    public void Configure(EntityTypeBuilder<UserDetails> builder)
    {
        base.Configure(builder);
        builder.Property(t => t.Id);
        builder.Property(t => t.UserId);
        builder.Property(t => t.Birthday);
        builder.Property(t => t.SocialName).HasMaxLength(100).IsRequired();
        builder.Property(t => t.NickName).HasMaxLength(50);
    }
}