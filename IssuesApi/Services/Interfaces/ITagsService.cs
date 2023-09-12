using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface ITagsService : IService
{
    Task<Result<TagOutputDTO>> Create(CreateTagDTO dto);
    Task<Result<TagOutputDTO>> Update(UpdateTagDTO dto);
    Task<Result<TagOutputDTO>> Get(long id);
    Task<Result<PageResult<TagOutputDTO>>> GetPage(PageFilter filter);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
}
