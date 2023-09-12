using AutoMapper;
using IssuesApi.Classes.Base.Interfaces;
namespace IssuesApi.Classes.Base;

public abstract class BaseService : IService
{
    protected readonly IMapper _mapper;

    public BaseService(IMapper mapper)
    {
        _mapper = mapper;
    }
}
