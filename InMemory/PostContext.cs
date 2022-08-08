using Microsoft.EntityFrameworkCore;

namespace InMemory;

public class PostContext : DbContext
{
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseInMemoryDatabase(databaseName: "Blog");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Post>().HasKey(p => p.Id);
    }
}