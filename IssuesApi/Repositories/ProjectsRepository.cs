using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;

namespace IssuesApi.Repositories;

public class ProjectsRepository : BaseRepository<Project>, IProjectsRepository
{
    public ProjectsRepository(IssuesDbContext context) : base(context)
    {
    }
}
