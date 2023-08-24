using IssuesApi.Classes.Pagination;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Classes.Base.Interfaces;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<Result<T>> Create(T entity);
    Task<Result<T>> Update(long id, T entity);
    ///<returns> A tuple with the list of results and total items count. </returns>
    Task<Result<(List<T> data, long total)>> GetPage(PageFilter filter);
}
