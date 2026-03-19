using DotNetWithKafka.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetWithKafka.Infrastructure.Config;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Roles> Roles { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<UserDetails> UserDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext)
            .Assembly);
    }
}