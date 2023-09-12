using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Filters;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using LanguageExt.Common;

namespace IssuesApi.Services.Interfaces;

public interface IUsersService : IService
{
    Task<Result<UserOutputDTO>> Create(CreateUserDTO dto);
    Task<Result<UserOutputDTO>> Update(UpdateUserDTO dto);
    Task<Result<UserOutputDTO>> Get(long id);
    Task<Result<bool>> SoftDelete(long id);
    Task<Result<bool>> HardDelete(long id);
    Task<Result<PageResult<UserOutputDTO>>> GetPage(UsersPageFilter filter);
}
