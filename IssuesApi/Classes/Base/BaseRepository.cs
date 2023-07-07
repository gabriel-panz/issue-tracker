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

        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Option<T>> Get(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public Task<OptionalResult<(List<T> data, long total)>> GetPage(PageFilter filter)
    {
        throw new NotImplementedException();
    }

    public Task HardDelete(long id)
    {
        throw new NotImplementedException();
    }

    public Task SoftDelete(long id)
    {
        throw new NotImplementedException();
    }
}
