using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;

namespace IssuesApi.Services;

public class ProjectsService : BaseService<ProjectDTO, Project>, IProjectsService
{
    public ProjectsService(IProjectsRepository repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}
