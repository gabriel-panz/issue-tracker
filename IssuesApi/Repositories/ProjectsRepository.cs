using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs;
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
        IMapper mapper
    )
        : base(context, mapper)
    {
    }

    public async Task<Result<Project>> Create(Project entity)
    {
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

    public async Task<Result<FilteredList<Project>>> GetPage(
        PageFilter filter)
    {
        var query = _context.Set<Project>()
            .AsQueryable();

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .ToListAsync();

        return new FilteredList<Project>(result, total);
    }

    public async Task<Option<ProjectOutputDTO>> Get(long id)
    {
        var result = await _context.Set<Project>()
            .Include(p => p.Issues)
                .ThenInclude(p => p.IssueTags)
                    .ThenInclude(p => p.Tag)
            .Where(p => p.Id == id)
            .Select(x => _mapper.Map<ProjectOutputDTO>(x))
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<bool> HardDelete(long id)
    {
        var result = await _context.Set<Project>()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<bool> SoftDelete(long id)
    {
        var result = await _context.Set<Project>()
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsEnabled, false)
                .SetProperty(p => p.DeletedAt, DateTime.Now));

        return result > 0;
    }
}
