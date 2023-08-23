using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs.Projects;
using LanguageExt;

namespace IssuesApi.Repositories.Interfaces;

public interface IProjectsRepository : IRepository<Project>
{ 
    Task<Option<ProjectOutputDTO>> Get2(long id);
}
