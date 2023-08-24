using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface ITagsRepository
{
    Task<Result<Tag>> Create(Tag entity);
    Task<Result<Tag>> Update(Tag entity);
    Task<Option<Tag>> Get(long id);

    ///<returns> A tuple with the list of results and total items count. </returns>
    Task<Result<FilteredList<Tag>>> GetPage(PageFilter filter);
    Task<bool> SoftDelete(long id);
    Task<bool> HardDelete(long id);
 }
