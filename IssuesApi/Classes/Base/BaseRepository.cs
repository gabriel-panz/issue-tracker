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

    public async Task<Result<T>> Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

        try
        {
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (System.Exception e)
        {
            return new(e);
        }
    }

    public async Task<Result<T>> Update(long id, T entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.UtcNow;

        _context.Set<T>().Update(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (System.Exception e)
        {
            return new(e);
        }

        return entity;
    }

    public async Task<Result<(List<T> data, long total)>> GetPage(
        PageFilter filter)
    {
        var query = _context.Set<T>().AsQueryable();

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .ToListAsync();

        return (result, total);
    }
}
