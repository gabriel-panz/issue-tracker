using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface IUsersRepository : IRepository<User>
{
    Task<Result<User>> Create(User entity);
    Task<Result<User>> Update(User entity);
    Task<Result<FilteredList<User>>> GetPage(UsersPageFilter filter);
    Task<Option<User>> Get(long id);
    Task<bool> SoftDelete(long id);
    Task<bool> HardDelete(long id);
}
