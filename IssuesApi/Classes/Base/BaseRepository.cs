using AutoMapper;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Context;
using IssuesApi.Utils;

namespace IssuesApi.Classes.Base;

public abstract class BaseRepository<T> : IRepository<T>
    where T : class
{
    protected readonly IssuesDbContext _context;
    protected readonly IMapper _mapper;
    private readonly HttpContext _httpContext;

    public BaseRepository(
        IssuesDbContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    protected long GetRequestUserId()
    {
        var userId = _httpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.UserId.ToString())?
            .Value;

        return userId is null
            ? throw new Exception("could not find userId in request context")
            : long.Parse(userId);
    }
}
