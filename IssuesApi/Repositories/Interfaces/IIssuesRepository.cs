using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters.Issues;
using IssuesApi.Domain.Inputs.Issues;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface IIssuesRepository : IRepository<IssueItem>
{
    Task<Result<IssueItem>> Create(IssueItem entity);
    Task<Result<IssueItem>> Update(IssueItem entity);
    ///<returns> A tuple with the list of results and total items count. </returns>
    Task<Result<FilteredList<IssueItem>>> GetPage(IssuesPageFilter filter);
    Task<Option<IssueItem>> Get(long id);
    Task<bool> SoftDelete(long id);
    Task<bool> HardDelete(long id);
    Task AddTags(UpdateTagsDTO dto);
    Task AddTags(long issueId, List<long> tagIds);
    Task RemoveTags(UpdateTagsDTO dto);
}
