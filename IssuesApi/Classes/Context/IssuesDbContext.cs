using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Classes.Context;

public class IssuesDbContext : DbContext
{
    public IssuesDbContext() : base()
    { }
    public IssuesDbContext(DbContextOptions options) : base(options)
    { }
}
