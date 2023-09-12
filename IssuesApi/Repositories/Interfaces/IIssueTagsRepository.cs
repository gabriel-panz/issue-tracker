using System.Linq.Expressions;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filter;
using LanguageExt.Common;

namespace IssuesApi.Repositories.Interfaces;

public interface IIssueTagsRepository : IRepository<IssueTag>
{
    Task<Result<IssueTag>> Create(IssueTag entity);
    Task<List<IssueTag>> List(IssueTagFilter filter);
    Task<List<IssueTag>> List(params Expression<Func<IssueTag, bool>>[] predicates);
    Task<List<TResult>> List<TResult>(Expression<Func<IssueTag, TResult>> selector, params Expression<Func<IssueTag, bool>>[] predicates);
    Task<bool> HardDelete(long id);
}
