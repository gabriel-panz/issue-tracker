using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Services.Interfaces;

public interface IProjectsService : IService<ProjectDTO, Project>
{  }
