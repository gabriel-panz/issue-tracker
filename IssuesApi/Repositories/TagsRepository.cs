using System.Linq.Expressions;
using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class TagsRepository : BaseRepository<Tag>, ITagsRepository
{
    public TagsRepository(
        IssuesDbContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor
        ) : base(context, mapper, httpContextAccessor)
    {
    }

    public async Task<Result<Tag>> Create(Tag entity)
    {
        entity.CreatedByUserId = GetRequestUserId();

        await _context.Set<Tag>().AddAsync(entity);

        try
        {
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            return new(e);
        }
    }

    public async Task<Option<Tag>> Get(long id)
    {
        return await _context.Set<Tag>()
            .Where(x => x.CreatedByUserId == GetRequestUserId())
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Result<FilteredList<Tag>>> GetPage(PageFilter filter)
    {
        var query = _context.Set<Tag>()
            .Where(x => x.CreatedByUserId == GetRequestUserId());

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .ToListAsync();

        return new FilteredList<Tag>(result, total);
    }

    public async Task<bool> HardDelete(long id)
    {
        var result = await _context.Set<Tag>()
            .Where(x => x.CreatedByUserId == GetRequestUserId())
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<List<Tag>> List(params Expression<Func<Tag, bool>>[] predicates)
    {
        var query = _context.Set<Tag>()
            .Where(x => x.CreatedByUserId == GetRequestUserId());

        foreach (var predicate in predicates)
        {
            query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task<bool> SoftDelete(long id)
    {
        var result = await _context.Set<Tag>()
            .Where(x => x.CreatedByUserId == GetRequestUserId())
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsEnabled, false)
                .SetProperty(p => p.DeletedAt, DateTime.Now));

        return result > 0;
    }

    public async Task<Result<Tag>> Update(Tag entity)
    {
        var tag = await _context.Set<Tag>()
            .Where(x => x.CreatedByUserId == GetRequestUserId())
            .FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (tag is null)
            return new(ResourceNotFoundException
                .Create(nameof(Tag), $"Id == {entity.Id}"));

        entity.UpdatedAt = DateTime.UtcNow;

        _context.Set<Tag>().Update(entity);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new(e);
        }

        return entity;
    }
}
