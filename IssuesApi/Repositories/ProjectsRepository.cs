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

    public async override Task<Option<Project>> Get(long id)
    {
        return await _context.Set<Project>()
            .IgnoreAutoIncludes()
            .Include(p => p.Issues)
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Option<ProjectOutputDTO>> Get2(long id)
    {
        return await _context.Set<Project>()
            .Select(p => new ProjectOutputDTO()
            {
                Id = p.Id,
                Description = p.Description,
                Title = p.Title,
                Issues = p.Issues
            })
            .FirstOrDefaultAsync();
    }
}
