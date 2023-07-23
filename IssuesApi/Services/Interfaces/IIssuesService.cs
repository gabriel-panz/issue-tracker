using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IIssuesService : IService<IssueItemDTO, IssueItem>
{
    Task<Result<PageResult<IssueItemDTO>>> GetPage(long projectId, PageFilter filter);
}
