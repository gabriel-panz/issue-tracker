using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters.Issues;
using IssuesApi.Domain.Inputs.Issues;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class IssuesService : BaseService<IssueItemDTO, IssueItem>
    , IIssuesService
{
    private new readonly IIssuesRepository _repository;
    public IssuesService(
        IIssuesRepository repository,
        IMapper mapper)
            : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<Result<PageResult<IssueItemDTO>>> GetPage(IssuesPageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<IssueItemDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<IssueItemDTO>>(result.data);
            return new PageResult<IssueItemDTO>(mappedData, filter, result.total);
        },
            Fail: exception => new(exception)
        );
    }

    public async Task<Result<IssueItemDTO>> Create(CreateIssueDTO dto)
    {
        var entity = _mapper.Map<IssueItem>(dto);
        var result = await _repository.Create(entity);

        return result.Match<Result<IssueItemDTO>>(
            Succ: s => _mapper.Map<IssueItemDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<IssueItemDTO>> Update(long id, CreateIssueDTO dto)
    {
        var entity = _mapper.Map<IssueItem>(dto);
        var result = await _repository.Update(id, entity);

        return result.Match<Result<IssueItemDTO>>(
            Succ: s => _mapper.Map<IssueItemDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<IssueItemDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<IssueItemDTO>>(
            Some: s => _mapper.Map<IssueItemDTO>(s),
            None: () => new(new ResourceNotFoundException())
        );
    }

    public async Task<Result<bool>> HardDelete(long id)
    {
        return await _repository.HardDelete(id);
    }

    public async Task<Result<bool>> SoftDelete(long id)
    {
        return await _repository.SoftDelete(id);
    }

    public async Task AddTags(UpdateTagsDTO dto)
    {
        await _repository.AddTags(dto);
    }

    public async Task RemoveTags(UpdateTagsDTO dto)
    {
        await _repository.RemoveTags(dto);
    }
}
