using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs;
using IssuesApi.Domain.Filter;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class ProjectsRepository
    : BaseRepository<Project>, IProjectsRepository
{
    public ProjectsRepository(
        IssuesDbContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor
    )
        : base(context, mapper, httpContextAccessor)
    { }

    public async Task<Result<Project>> Create(Project entity)
    {
        entity.CreatedByUserId = GetRequestUserId();
        await _context.Set<Project>().AddAsync(entity);

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

    public async Task<Result<Project>> Update(Project entity)
    {
        var userId = GetRequestUserId();
        var existingEntity = await _context
            .Set<Project>()
            .Where(x => x.CreatedByUserId == userId)
            .FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (existingEntity is null)
            return new(ResourceNotFoundException.Create(nameof(entity), $"Id == {entity.Id}"));

        if (existingEntity.CreatedByUserId != userId)
            return new(ResourceNotFoundException.Create(nameof(entity), $"Id == {entity.Id}"));

        entity.UpdatedAt = DateTime.UtcNow;

        _context.Set<Project>().Update(entity);

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

    public async Task<Result<FilteredList<ProjectOutputDTO>>> GetPage(
        ProjectFilter filter)
    {
        var userId = GetRequestUserId();

        var query = _context.Set<Project>()
            .AsQueryable()
            .Where(x => x.CreatedByUserId == userId);

        if (filter.Description is not null)
        {
            query = query.Where(x
                => x.Description != null
                && x.Description.ToLower()
                    .Contains(filter.Description.ToLower()));
        }

        if (filter.Title is not null)
        {
            query = query.Where(x
                => x.Title.ToLower().Contains(filter.Title.ToLower()));
        }

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .Select(x => new ProjectOutputDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                OpenIssues = x.Issues.Count(x => x.Status == IssueStatus.OPEN),
                ActiveIssues = x.Issues.Count(x => x.Status == IssueStatus.IN_PROGRESS),
                ClosedIssues = x.Issues.Count(x => x.Status == IssueStatus.CLOSED)
            })
            .ToListAsync();

        return new FilteredList<ProjectOutputDTO>(result, total);
    }

    public async Task<Option<ProjectOutputDTO>> Get(long id)
    {
        var userId = GetRequestUserId();

        var result = await _context.Set<Project>()
            .Include(p => p.Issues)
                .ThenInclude(p => p.IssueTags)
                    .ThenInclude(p => p.Tag)
            .Where(p => p.CreatedByUserId == userId)
            .Where(p => p.Id == id)
            .Select(x => new ProjectOutputDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                OpenIssues = x.Issues.Count(x => x.Status == IssueStatus.OPEN),
                ActiveIssues = x.Issues.Count(x => x.Status == IssueStatus.IN_PROGRESS),
                ClosedIssues = x.Issues.Count(x => x.Status == IssueStatus.CLOSED)
            })
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<Result<bool>> HardDelete(long id)
    {
        var userId = GetRequestUserId();

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var project = await _context.Set<Project>()
                .Where(x => x.CreatedByUserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (project is null)
                return new(ResourceNotFoundException.Create(
                    nameof(Project), $"Id == {id}"));

            var issues = await _context.Set<Project>()
                .Where(x => x.CreatedByUserId == userId)
                .Where(x => x.Id == id)
                .SelectMany(x => x.Issues)
                .ToListAsync();

            _context.Set<IssueItem>().RemoveRange(issues);
            _context.Set<Project>().Remove(project);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            return new(e);
            throw;
        }
    }

    public async Task<Result<bool>> SoftDelete(long id)
    {
        var userId = GetRequestUserId();

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var affectedProjects = await _context.Set<Project>()
                .Where(x => x.CreatedByUserId == userId)
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsEnabled, false)
                    .SetProperty(p => p.DeletedAt, DateTime.Now));

            if (affectedProjects <= 0)
                throw ResourceNotFoundException.Create(nameof(Project), $"id == {id}");

            var affectedIssues = await _context.Set<IssueItem>()
                .Where(x => x.ProjectId == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.IsEnabled, false)
                    .SetProperty(p => p.DeletedAt, DateTime.Now));

            await transaction.CommitAsync();

            return true;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            return new(e);
        }
    }
}
