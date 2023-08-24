using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Inputs.Projects;
using IssuesApi.Domain.Outputs.Projects;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IProjectsService
{
    Task<Result<ProjectDTO>> Create(CreateProjectDTO dto);
    Task<Result<ProjectDTO>> Update(UpdateProjectDTO dto);
    Task<Result<ProjectOutputDTO>> Get(long id);
    Task<Result<PageResult<ProjectDTO>>> GetPage(PageFilter filter);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
}
