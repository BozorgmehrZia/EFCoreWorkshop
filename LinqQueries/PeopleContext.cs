using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LinqQueries;

public class PeopleContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<House> Houses { get; set; }
    
    private static readonly ILoggerFactory s_myLoggerFactory =
        LoggerFactory.Create(
            builder =>
            {
                builder.AddConsole();
            }
        );

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Server=localhost;Port=5433;Database=People;Username=postgres;Password=0024936219")
            .UseLoggerFactory(s_myLoggerFactory);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Person>().HasKey(p => p.Id);
        modelBuilder.Entity<Car>().HasKey(p => p.Id);
        modelBuilder.Entity<House>().HasKey(p => p.Id);
    }
}