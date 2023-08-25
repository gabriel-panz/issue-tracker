using AutoMapper;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Context;

namespace IssuesApi.Classes.Base;

public abstract class BaseRepository<T> : IRepository<T>
    where T : class, IEntity
{
    protected readonly IssuesDbContext _context;
    protected readonly IMapper _mapper;

    public BaseRepository(IssuesDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
