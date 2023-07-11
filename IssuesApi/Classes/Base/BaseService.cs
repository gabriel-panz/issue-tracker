using AutoMapper;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using LanguageExt;
using LanguageExt.Common;

namespace IssuesApi.Classes.Base;

public class BaseService<DTO, T> : IService<DTO, T>
    where T : class, IEntity
{
    private readonly IRepository<T> _repository;
    private readonly IMapper _mapper;

    public BaseService(IRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<DTO>> Create(DTO dto)
    {
        var entity = _mapper.Map<T>(dto);
        var result = await _repository.Create(entity);
        return _mapper.Map<DTO>(result);
    }

    public async Task<Result<DTO>> Update(long id, DTO dto)
    {
        var entity = _mapper.Map<T>(dto);
        var result = await _repository.Update(id, entity);
        return _mapper.Map<DTO>(result);
    }

    public async Task<Result<DTO>> Get(long id)
    {
        var result = await _repository.Get(id);
        return _mapper.Map<DTO>(result);
    }

    public async Task<Result<PageResult<DTO>>> GetPage(PageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<DTO>>>(
            Some: (result) =>
        {
            var mappedData = _mapper.Map<List<DTO>>(result.data);
            return new PageResult<DTO>(mappedData, filter, result.total);
        },
            None: () => PageResult<DTO>.Empty,
            Fail: exception => new(exception)
        );
    }

    public async Task<Result<bool>> HardDelete(long id)
    {
        var option = await _repository.Get(id);
        if (option.IsNone)
            return new(new ResourceNotFoundException());
        if (option.IsSome)
            await _repository.HardDelete((T)option);

        return new(true);
    }

    public async Task<Result<bool>> SoftDelete(long id)
    {
        var option = await _repository.Get(id);
        if (option.IsNone)
            return new(new ResourceNotFoundException());
        if (option.IsSome)
            await _repository.SoftDelete((T)option);

        return new(true);
    }
}
