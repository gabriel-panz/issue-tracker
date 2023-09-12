using System.Linq.Expressions;
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
    Task<Result<FilteredList<Tag>>> GetPage(PageFilter filter);
    Task<List<Tag>> List(params Expression<Func<Tag, bool>>[] predicates);
    Task<bool> SoftDelete(long id);
    Task<bool> HardDelete(long id);
}
