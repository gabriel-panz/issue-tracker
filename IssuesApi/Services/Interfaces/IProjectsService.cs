using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IProjectsService
{
    Task<Result<ProjectOutputDTO>> Create(CreateProjectDTO dto);
    Task<Result<ProjectOutputDTO>> Update(UpdateProjectDTO dto);
    Task<Result<ProjectOutputDTO>> Get(long id);
    Task<Result<PageResult<ProjectOutputDTO>>> GetPage(PageFilter filter);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
}