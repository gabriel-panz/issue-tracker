using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs.Projects;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class ProjectsRepository : BaseRepository<Project>, IProjectsRepository
{
    public ProjectsRepository(IssuesDbContext context) : base(context)
    {
    }

    public async Task<Option<ProjectOutputDTO>> Get(long id)
    {
        return await _context.Set<Project>()
            .Where(p => p.Id == id)
            .Select(p => new ProjectOutputDTO()
            {
                Id = p.Id,
                Description = p.Description,
                Title = p.Title,
                Issues = p.Issues
            })
            .FirstOrDefaultAsync();
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
