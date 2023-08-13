using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters.Issues;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface IIssuesRepository : IRepository<IssueItem>
{
    Task<Result<(List<IssueItem> data, long total)>> GetPage(IssuesPageFilter filter);
}
