using Microsoft.EntityFrameworkCore;

namespace MinimalApiReprPattern.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> Todos => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem { Id = 1, Title = "Learn REPR pattern", IsComplete = true },
            new TodoItem { Id = 2, Title = "Build Minimal API", IsComplete = true },
            new TodoItem { Id = 3, Title = "Deploy to production", IsComplete = false }
        );
    }
}
