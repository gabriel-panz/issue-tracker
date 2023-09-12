using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using IssuesApi.Domain.Filter;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IProjectsService : IService
{
    Task<Result<ProjectOutputDTO>> Create(CreateProjectDTO dto);
    Task<Result<ProjectOutputDTO>> Update(UpdateProjectDTO dto);
    Task<Result<ProjectOutputDTO>> Get(long id);
    Task<Result<PageResult<ProjectOutputDTO>>> GetPage(ProjectFilter filter);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
}