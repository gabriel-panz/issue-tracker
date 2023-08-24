using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Pagination;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Classes.Base;

public abstract class BaseRepository<T> : IRepository<T>
    where T : class, IEntity
{
    protected readonly IssuesDbContext _context;
    public BaseRepository(IssuesDbContext context)
    {
        _context = context;
    }
}
