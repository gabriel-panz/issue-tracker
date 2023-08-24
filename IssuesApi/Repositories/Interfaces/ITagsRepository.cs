using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Domain.Entities;
using LanguageExt;

namespace IssuesApi.Repositories.Interfaces;

public interface ITagsRepository : IRepository<Tag>
{
    Task<Option<Tag>> Get(long id);
    Task<bool> SoftDelete(long id);
    Task<bool> HardDelete(long id);
 }
