using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Classes.Context;

public class IssuesDbContext : DbContext
{
    public IssuesDbContext() : base()
    { 
        if (!Database.EnsureCreated())
            Database.Migrate();
    }
    public IssuesDbContext(DbContextOptions options) : base(options)
    { 
        if (Database.EnsureCreated())
            Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(Domain.Entities.IssueItem).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
