using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters.Issues;
using IssuesApi.Domain.Inputs.Issues;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class IssuesRepository : BaseRepository<IssueItem>, IIssuesRepository
{
    public IssuesRepository(IssuesDbContext context) : base(context)
    {
    }

    public async Task<Result<IssueItem>> Create(IssueItem entity)
    {
        await _context.Set<IssueItem>().AddAsync(entity);

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

    public async Task<Result<IssueItem>> Update(IssueItem entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _context.Set<IssueItem>().Update(entity);

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

    public async Task<Option<IssueItem>> Get(long id)
    {
        return await _context.Set<IssueItem>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> HardDelete(long id)
    {
        var result = await _context.Set<IssueItem>()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<bool> SoftDelete(long id)
    {
        var result = await _context.Set<IssueItem>()
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsEnabled, false)
                .SetProperty(p => p.DeletedAt, DateTime.Now));

        return result > 0;
    }

    public async Task AddTags(UpdateTagsDTO dto)
    {
        var issueTags = new List<IssueTag>();
        foreach (var id in dto.TagIds)
        {
            issueTags.Add(
                new()
                {
                    IssueId = dto.IssueId,
                    TagId = id
                }
            );
        }

        await _context.Set<IssueTag>()
            .AddRangeAsync(issueTags);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveTags(UpdateTagsDTO dto)
    {
        var issueTags = await _context.Set<IssueTag>()
            .Where(x => x.IssueId == dto.IssueId)
            .Where(x => dto.TagIds.Contains(x.TagId))
            .ToListAsync();

        _context.Set<IssueTag>()
            .RemoveRange(issueTags);

        await _context.SaveChangesAsync();
    }

    public async Task<Result<FilteredList<IssueItem>>> GetPage(
        long projectId,
        PageFilter filter)
    {
        var query = _context.Set<Project>()
            .Where(x => x.Id == projectId);

        var total = await query.CountAsync();

        var result = await query
            .Select(p => p.Issues
                .Skip(filter.Skip())
                .Take(filter.Size)
                .ToList())
            .FirstAsync();

        return new FilteredList<IssueItem>(result, total);
    }

    public async Task<Result<FilteredList<IssueItem>>> GetPage(
        IssuesPageFilter filter)
    {
        var query = _context.Set<IssueItem>()
           .Where(x => x.ProjectId == filter.ProjectId);

        if (filter.Status.Any())
            query = query.Where(x =>
                filter.Status.Contains(x.Status));

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .ToListAsync();

        return new FilteredList<IssueItem>(result, total);
    }

    public Task AddTags(long issueId, List<long> tagIds)
    {
        return AddTags(new UpdateTagsDTO()
        {
            IssueId = issueId,
            TagIds = tagIds
        });
    }
}
