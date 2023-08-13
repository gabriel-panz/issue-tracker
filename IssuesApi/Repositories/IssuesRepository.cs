using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters.Issues;
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
                .Skip(filter.Skip())
                .Take(filter.Size)
                .ToList())
            .FirstAsync();

        return (result, total);
    }

    public async Task<Result<(List<IssueItem> data, long total)>> GetPage(
        IssuesPageFilter filter)
    {
        var query = _context.Set<IssueItem>()
           .Where(x => x.ProjectId == filter.ProjectId);

        if (filter.Status.Any())
            query = query.Where(x =>
                filter.Status.Contains(x.Status));

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .ToListAsync();

        return (result, total);
    }
}
