using IssuesApi.Classes.Pagination;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Classes.Base.Interfaces;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<Result<T>> Create(T entity);
    Task<Result<T>> Update(long id, T entity);
    Task<Option<T>> Get(long id);
    ///<returns> A tuple with the list of results and total items count. </returns>
    Task<OptionalResult<(List<T> data, long total)>> GetPage(PageFilter filter);
    Task SoftDelete(T entity);
    Task HardDelete(T entity);
}
