using System.Linq.Expressions;
using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Context;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters;
using IssuesApi.Repositories.Interfaces;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace IssuesApi.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    public UsersRepository(
        IssuesDbContext context,
        IMapper mapper
        ) : base(context, mapper)
    {
    }

    public async Task<Result<User>> Create(User entity)
    {
        await _context.Set<User>().AddAsync(entity);

        try
        {
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            return new(e);
        }
    }

    public async Task<Result<User>> Update(User entity)
    {
        try
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _context.Set<User>().Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new(e);
        }

        return entity;
    }

    public async Task<Option<User>> Get(long id)
    {
        return await _context.Set<User>()
            .FindAsync(id);
    }

    public async Task<bool> HardDelete(long id)
    {
        var result = await _context.Set<User>()
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return result > 0;
    }

    public async Task<bool> SoftDelete(long id)
    {
        var result = await _context.Set<User>()
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.IsEnabled, false)
                .SetProperty(p => p.DeletedAt, DateTime.Now));

        return result > 0;
    }

    public async Task<Result<FilteredList<User>>> GetPage(
        UsersPageFilter filter)
    {
        var query = _context.Set<User>()
           .AsQueryable();

        if (filter.Email is not null)
            query = query.Where(x
                => x.Email != null
                && filter.Email.Contains(x.Email));

        if (filter.Nickname is not null)
            query = query.Where(x
                => x.Nickname != null
                && filter.Nickname.Contains(x.Nickname));

        if (filter.Login is not null)
            query = query.Where(x
                => x.Login != null
                && filter.Login.Contains(x.Login));

        var total = await query.CountAsync();

        var result = await query
            .Skip(filter.Skip())
            .Take(filter.Size)
            .ToListAsync();

        return new FilteredList<User>(result, total);
    }

    public async Task<List<User>> List(params Expression<Func<User, bool>>[] predicates)
    {
        var query = _context.Set<User>().AsQueryable();
        foreach (var item in predicates)
        {
            query = query.Where(item);
        }
        return await query.ToListAsync();
    }
}
