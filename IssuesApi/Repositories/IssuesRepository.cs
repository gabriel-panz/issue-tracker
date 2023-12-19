using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters;
using IssuesApi.Domain.Outputs;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class IssuesRepository : BaseRepository<IssueItem>, IIssuesRepository
{
    public IssuesRepository(
        IssuesDbContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor
        ) : base(context, mapper, httpContextAccessor)
    {
    }

    public async Task<Result<IssueItem>> Create(IssueItem entity)
    {
        var project = await _context
            .Set<Project>()
            .Where(x => x.CreatedByUserId == GetRequestUserId())
            .FirstOrDefaultAsync(x => x.Id == entity.ProjectId);

        if (project is null) return new(ResourceNotFoundException
                .Create(nameof(Project), $"Id == {entity.ProjectId}"));

        var validatedAddTags = await _context.Set<Tag>()
            .Where(x => x.CreatedByUserId == GetRequestUserId())
            .Where(x => entity.IssueTags.Select(x => x.TagId).Contains(x.Id))
            .ToListAsync();

        entity.IssueTags = new List<IssueTag>();
        foreach (var item in validatedAddTags)
        {
            entity.IssueTags.Add(new()
            {
                IssueId = entity.Id,
                TagId = item.Id
            });
        }

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
        try
        {
            var project = await _context
                .Set<Project>()
                .Where(x => x.CreatedByUserId == GetRequestUserId())
                .FirstOrDefaultAsync(x => x.Id == entity.ProjectId);

            if (project is null) return new(ResourceNotFoundException
                    .Create(nameof(Project), $"Id == {entity.ProjectId}"));

            entity.UpdatedAt = DateTime.UtcNow;

            var tags = await _context.Set<IssueTag>()
                .AsNoTracking()
                .Where(x => x.IssueId == entity.Id)
                .ToListAsync();

            var tagIds = tags.Select(x => x.TagId);
            var newTagIds = entity.IssueTags.Select(x => x.TagId);

            var addTags = entity.IssueTags
                .Where(x => !tagIds.Contains(x.TagId))
                .ToList();

            var removeTags = tags.Where(x => !newTagIds.Contains(x.TagId));

            var validatedAddTags = await _context.Set<Tag>()
                .Where(x => x.CreatedByUserId == GetRequestUserId())
                .Where(x => addTags.Select(x => x.TagId).Contains(x.Id))
                .ToListAsync();

            entity.IssueTags = new List<IssueTag>();
            foreach (var item in validatedAddTags)
            {
                entity.IssueTags.Add(new()
                {
                    IssueId = entity.Id,
                    TagId = item.Id
                });
            }

            _context.Set<IssueTag>().RemoveRange(removeTags);

            _context.Set<IssueItem>().Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new(e);
        }

        return entity;
    }

    public async Task<Option<IssueItemOutputDTO>> Get(long id)
    {
        var q = _context.Set<IssueItem>()
            .Include(x => x.IssueTags)
                .ThenInclude(x => x.Tag)
            .Where(x => x.Project!.CreatedByUserId == GetRequestUserId())
            .Where(x => x.Id == id)
            .Select(issue => new IssueItemOutputDTO
            {
                Id = issue.Id,
                Description = issue.Description,
                ProjectId = issue.ProjectId,
                Title = issue.Title,
                Status = issue.Status,
                Tags = issue.IssueTags
                    .Select(tag => _mapper.Map<TagOutputDTO>(tag.Tag))
                    .ToList()
            });

        return await q.FirstOrDefaultAsync();
    }

    public async Task<bool> HardDelete(long id)
    {
        var result = await _context.Set<IssueItem>()
            .Where(x => x.Project!.CreatedByUserId == GetRequestUserId())
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<bool> SoftDelete(long id)
    {
        var result = await _context.Set<IssueItem>()
            .Where(x => x.Project!.CreatedByUserId == GetRequestUserId())
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsEnabled, false)
                .SetProperty(p => p.DeletedAt, DateTime.Now));

        return result > 0;
    }

    public async Task<Result<FilteredList<IssueItemOutputDTO>>> GetPage(
        IssuesPageFilter filter)
    {
        var query = _context.Set<IssueItem>()
            .Where(x => x.Project!.CreatedByUserId == GetRequestUserId())
            .Where(x => x.ProjectId == filter.ProjectId);

        if (filter.Status.Any())
            query = query.Where(x =>
                filter.Status.Contains(x.Status));

        if (filter.TagIds.Any())
        {
            query = query
                .Include(x => x.IssueTags);

            // Exclusive search
            foreach (var id in filter.TagIds)
                query = query
                    .Where(x => x.IssueTags.Any(y => y.TagId == id));

            // Inclusive search
            // query = query
            //     .Include(x => x.IssueTags)
            //     .Where(x => x.IssueTags
            //         .Any(y => filter.TagIds.Contains(y.TagId)));
        }

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .Select(issue => new IssueItemOutputDTO
            {
                Id = issue.Id,
                Description = issue.Description,
                ProjectId = issue.ProjectId,
                Title = issue.Title,
                Status = issue.Status,
                Tags = issue.IssueTags
                    .Select(tag => _mapper.Map<TagOutputDTO>(tag.Tag))
                    .ToList()
            })
            .ToListAsync();

        return new FilteredList<IssueItemOutputDTO>(result, total);
    }
}
