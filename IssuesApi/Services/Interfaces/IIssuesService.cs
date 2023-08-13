using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Inputs.Issues;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IIssuesService //: IService<IssueItemDTO, IssueItem>
{
    Task<Result<IssueItemDTO>> Create(CreateIssueDTO dto);
    Task<Result<IssueItemDTO>> Update(long id, CreateIssueDTO dto);
    Task<Result<IssueItemDTO>> Get(long id);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
    Task<Result<PageResult<IssueItemDTO>>> GetPage(long projectId, PageFilter filter);
}
