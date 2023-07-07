using IssuesApi.Classes.Pagination;
using LanguageExt.Common;

namespace IssuesApi.Classes.Base.Interfaces;

public interface IService<DTO, T>
    where T : class, IEntity
{
    Task<Result<DTO>> Create(DTO dto);
    Task<Result<DTO>> Update(long id, DTO dto);
    Task<Result<DTO>> Get(long id);
    Task<Result<PageResult<DTO>>> GetPage(PageFilter filter);
    Task SoftDelete(long id);
    Task HardDelete(long id);
}
