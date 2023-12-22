using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class TagsService
    : BaseService, ITagsService
{
    private readonly ITagsRepository _repository;

    public TagsService(ITagsRepository repository, IMapper mapper)
        : base(mapper)
    {
        _repository = repository;
    }

    public async Task<Result<TagOutputDTO>> Create(CreateTagDTO dto)
    {
        var entity = _mapper.Map<Tag>(dto);
        var result = await _repository.Create(entity);
        return result.Match<Result<TagOutputDTO>>(
            Succ: s => _mapper.Map<TagOutputDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<TagOutputDTO>> Update(UpdateTagDTO dto)
    {
        var entity = _mapper.Map<Tag>(dto);
        var result = await _repository.Update(entity);

        return result.Match<Result<TagOutputDTO>>(
            Succ: s => _mapper.Map<TagOutputDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<TagOutputDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<TagOutputDTO>>(
            Some: s => _mapper.Map<TagOutputDTO>(s),
            None: () => new(ResourceNotFoundException.Create("Tag", $"Id == {id}"))
        );
    }

    public async Task<Result<PageResult<TagOutputDTO>>> GetPage(PageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<TagOutputDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<TagOutputDTO>>(result.Data);
            return new PageResult<TagOutputDTO>(mappedData, filter, result.TotalCount);
        },
            Fail: exception => new(exception)
        );
    }

    public async Task<Result<bool>> HardDelete(long id)
    {
        return await _repository.HardDelete(id);
    }

    public async Task<Result<bool>> SoftDelete(long id)
    {
        var res = await _repository.SoftDelete(id);

        if (!res) return new(ResourceNotFoundException
            .Create("Tag", $"Id == {id}"));

        return true;
    }
}
