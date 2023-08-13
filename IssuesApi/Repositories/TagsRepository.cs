using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;

namespace IssuesApi.Repositories;

public class TagsRepository : BaseRepository<Tag>, ITagsRepository
{
    public TagsRepository(IssuesDbContext context) : base(context)
    {
    }
}
