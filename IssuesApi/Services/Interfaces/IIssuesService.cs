using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Filters;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IIssuesService
{
    Task<Result<IssueItemOutputDTO>> Create(CreateIssueDTO dto);
    Task<Result<IssueItemOutputDTO>> Update(UpdateIssueDTO dto);
    Task<Result<IssueItemOutputDTO>> Get(long id);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
    Task<Result<PageResult<IssueItemOutputDTO>>> GetPage(IssuesPageFilter filter);
}
