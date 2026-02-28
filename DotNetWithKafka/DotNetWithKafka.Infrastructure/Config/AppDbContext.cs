using Microsoft.EntityFrameworkCore;

namespace DotNetWithKafka.Infrastructure.Config;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}