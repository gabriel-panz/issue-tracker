using IssuesApi.Classes.Base;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Services.Interfaces;

namespace IssuesApi.Controllers;

public class ProjectsController : BaseController<ProjectDTO, Project>
{
    public ProjectsController(IProjectsService service) : base(service)
    {
    }
}
