using AutoMapper;
using IssuesApi.Classes.Base.Interfaces;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using LanguageExt.Common;

namespace IssuesApi.Classes.Base;

public abstract class BaseService<DTO, T> : IService<DTO, T>
    where T : class, IEntity
{
    protected readonly IRepository<T> _repository;
    protected readonly IMapper _mapper;

    public BaseService(IRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<DTO>> Create(DTO dto)
    {
        var entity = _mapper.Map<T>(dto);
        var result = await _repository.Create(entity);
        return result.Match<Result<DTO>>(
            Succ: s => _mapper.Map<DTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<DTO>> Update(long id, DTO dto)
    {
        var entity = _mapper.Map<T>(dto);
        var result = await _repository.Update(id, entity);

        return result.Match<Result<DTO>>(
            Succ: s => _mapper.Map<DTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<DTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<DTO>>(
            Some: s => _mapper.Map<DTO>(s),
            None: () => new(new ResourceNotFoundException())
        );
    }

    public async Task<Result<PageResult<DTO>>> GetPage(PageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<DTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<DTO>>(result.data);
            return new PageResult<DTO>(mappedData, filter, result.total);
        },
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
