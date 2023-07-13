using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Classes.Context;

public class IssuesDbContext : DbContext
{
    public IssuesDbContext() : base()
    { 
        Database.Migrate();
    }
    public IssuesDbContext(DbContextOptions options) : base(options)
    { 
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(Domain.Entities.IssueItem).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
