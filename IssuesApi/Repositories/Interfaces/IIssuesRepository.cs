using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters.Issues;
using IssuesApi.Domain.Inputs.Issues;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface IIssuesRepository : IRepository<IssueItem>
{
    Task AddTags(UpdateTagsDTO dto);
    Task AddTags(long issueId, List<long> tagIds);
    Task RemoveTags(UpdateTagsDTO dto);
    Task<Result<(List<IssueItem> data, long total)>> GetPage(IssuesPageFilter filter);
}
