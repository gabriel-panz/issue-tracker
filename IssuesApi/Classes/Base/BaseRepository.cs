using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Pagination;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Classes.Base;

public class BaseRepository<T> : IRepository<T>
    where T : class, IEntity
{
    private readonly IssuesDbContext _context;
    public BaseRepository(IssuesDbContext context)
    {
        _context = context;
    }

    public async Task<OptionalResult<T>> Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<OptionalResult<T>> Update(long id, T entity)
    {
        entity.Id = id;
        entity.UpdatedAt = DateTime.UtcNow;

        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Option<T>> Get(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<OptionalResult<(List<T> data, long total)>> GetPage(
        PageFilter filter)
    {
        var query = _context.Set<T>().AsQueryable();

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip)
            .Take(filter.Size)
            .ToListAsync();

        return (result, total);
    }

    public async Task HardDelete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task SoftDelete(T entity)
    {
        entity.IsEnabled = false;
        entity.DeletedAt = DateTime.UtcNow;
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
