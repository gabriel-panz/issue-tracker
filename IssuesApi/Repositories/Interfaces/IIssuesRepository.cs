using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface IIssuesRepository : IRepository<IssueItem>
{
    Task<Result<IssueItem>> Create(IssueItem entity);
    Task<Result<IssueItem>> Update(IssueItem entity);
    Task<Result<FilteredList<IssueItemOutputDTO>>> GetPage(IssuesPageFilter filter);
    Task<Option<IssueItemOutputDTO>> Get(long id);
    Task<bool> SoftDelete(long id);
    Task<bool> HardDelete(long id);
}
