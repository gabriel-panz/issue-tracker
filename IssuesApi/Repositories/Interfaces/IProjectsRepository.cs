using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface IProjectsRepository
{
    Task<Result<Project>> Create(Project entity);
    Task<Result<Project>> Update(Project entity);
    Task<Option<ProjectOutputDTO>> Get(long id);
    Task<Result<FilteredList<ProjectOutputDTO>>> GetPage(PageFilter filter);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
}
