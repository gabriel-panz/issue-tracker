using System.Linq.Expressions;
using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filter;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class IssueTagsRepository : BaseRepository<IssueTag>, IIssueTagsRepository
{
    public IssueTagsRepository(
        IssuesDbContext context,
        IMapper mapper
        ) : base(context, mapper)
    {
    }

    public async Task<Result<IssueTag>> Create(IssueTag entity)
    {
        await _context.Set<IssueTag>().AddAsync(entity);

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

    public async Task<List<IssueTag>> List(IssueTagFilter filter)
    {
        var query = _context.Set<IssueTag>().AsQueryable();

        if (filter.TagIds is not null)
            query = query.Where(x => filter.TagIds.Contains(x.TagId));

        if (filter.IssueIds is not null)
            query = query.Where(x => filter.IssueIds.Contains(x.IssueId));

        return await query.ToListAsync();
    }

    public async Task<bool> HardDelete(long id)
    {
        var result = await _context.Set<IssueTag>()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<List<IssueTag>> List(
        Expression<Func<IssueTag, bool>>[] predicates)
    {
        var query = _context.Set<IssueTag>().AsQueryable();

        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }
        return await query.ToListAsync();
    }

    public async Task<List<TResult>> List<TResult>(
        Expression<Func<IssueTag, TResult>> selector,
        Expression<Func<IssueTag, bool>>[] predicates)
    {
        var query = _context.Set<IssueTag>()
            .AsQueryable();

        foreach (var predicate in predicates)
        {
            query = query.Where(predicate);
        }

        return await query
            .Include(x => x.IssueItem)
            .Include(x => x.Tag)
            .Select(selector)
            .ToListAsync();
    }
}
