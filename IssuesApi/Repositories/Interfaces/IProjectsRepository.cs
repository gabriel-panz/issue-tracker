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

    ///<returns> A tuple with the list of results and total items count. </returns>
    Task<Result<FilteredList<Project>>> GetPage(PageFilter filter);
    Task<bool> SoftDelete(long id);
    Task<bool> HardDelete(long id);
}
