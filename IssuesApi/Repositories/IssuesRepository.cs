using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class IssuesRepository : BaseRepository<IssueItem>, IIssuesRepository
{
    public IssuesRepository(IssuesDbContext context) : base(context)
    {
    }

    public async Task<Result<(List<IssueItem> data, long total)>> GetPage(
        long projectId,
        PageFilter filter)
    {
        var query = _context.Set<Project>()
            .Where(x => x.Id == projectId);

        var total = await query.CountAsync();

        var result = await query
            .Select(p => p.ProjectItems
                .Skip(filter.Skip)
                .Take(filter.Size)
                .ToList())
            .FirstAsync();

        return (result, total);
    }
}
