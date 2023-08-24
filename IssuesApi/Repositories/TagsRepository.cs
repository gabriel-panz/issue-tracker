using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class TagsRepository : BaseRepository<Tag>, ITagsRepository
{
    public TagsRepository(IssuesDbContext context) : base(context)
    {
    }
    public async Task<Option<Tag>> Get(long id)
    {
        return await _context.Set<Tag>()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> HardDelete(long id)
    {
        var result = await _context.Set<Tag>()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<bool> SoftDelete(long id)
    {
        var result = await _context.Set<Tag>()
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsEnabled, false)
                .SetProperty(p => p.DeletedAt, DateTime.Now));

        return result > 0;
    }
}
