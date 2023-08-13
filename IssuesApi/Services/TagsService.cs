using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs.Tags;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class TagsService : BaseService<TagDTO, Tag>, ITagsService
{
    public TagsService(ITagsRepository repository, IMapper mapper)
        : base(repository, mapper)
    {
    }

    public async Task<Result<TagDTO>> Create(CreateTagDTO dto)
    {
        var entity = _mapper.Map<Tag>(dto);
        var result = await _repository.Create(entity);
        return result.Match<Result<TagDTO>>(
            Succ: s => _mapper.Map<TagDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<TagDTO>> Update(long id, CreateTagDTO dto)
    {
        var entity = _mapper.Map<Tag>(dto);
        var result = await _repository.Update(id, entity);

        return result.Match<Result<TagDTO>>(
            Succ: s => _mapper.Map<TagDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<TagDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<TagDTO>>(
            Some: s => _mapper.Map<TagDTO>(s),
            None: () => new(new ResourceNotFoundException())
        );
    }

    public async Task<Result<PageResult<TagDTO>>> GetPage(PageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<TagDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<TagDTO>>(result.data);
            return new PageResult<TagDTO>(mappedData, filter, result.total);
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
            await _repository.HardDelete((Tag)option);

        return new(true);
    }

    public async Task<Result<bool>> SoftDelete(long id)
    {
        var option = await _repository.Get(id);

        if (option.IsNone)
            return new(new ResourceNotFoundException());
        if (option.IsSome)
            await _repository.SoftDelete((Tag)option);

        return new(true);
    }
}
