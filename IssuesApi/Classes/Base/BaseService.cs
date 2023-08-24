using AutoMapper;
using IssuesApi.Classes.Base.Interfaces;

namespace IssuesApi.Classes.Base;

public abstract class BaseService<DTO, T>
    where T : class, IEntity
{
    protected readonly IMapper _mapper;

    public BaseService(IMapper mapper)
    {
        _mapper = mapper;
    }
}
