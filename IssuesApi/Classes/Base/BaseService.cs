using AutoMapper;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using LanguageExt.Common;

namespace IssuesApi.Classes.Base;

public abstract class BaseService<DTO, T> //: IService<DTO, T>
    where T : class, IEntity
{
    protected readonly IRepository<T> _repository;
    protected readonly IMapper _mapper;

    public BaseService(IRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
}
