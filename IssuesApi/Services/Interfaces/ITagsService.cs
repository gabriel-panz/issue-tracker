using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Inputs.Tags;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface ITagsService
{
    Task<Result<TagDTO>> Create(CreateTagDTO dto);
    Task<Result<TagDTO>> Update(long id, CreateTagDTO dto);
    Task<Result<TagDTO>> Get(long id);
    Task<Result<PageResult<TagDTO>>> GetPage(PageFilter filter);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
}
