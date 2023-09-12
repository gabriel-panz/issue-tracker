using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filter;
using IssuesApi.Domain.Filters;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class IssuesService : BaseService
    , IIssuesService
{
    private readonly IIssuesRepository _repository;
    private readonly IIssueTagsRepository _issueTagsRepository;
    private readonly ITagsRepository _tagsRepository;
    public IssuesService(
        IIssuesRepository repository,
        IMapper mapper,
        IIssueTagsRepository issueTagsRepository,
        ITagsRepository tagsRepository)
            : base(mapper)
    {
        _repository = repository;
        _issueTagsRepository = issueTagsRepository;
        _tagsRepository = tagsRepository;
    }

    private async Task<Result<IssueItemOutputDTO>> GetResult(IssueItem issueItem)
    {
        var mappedResult = _mapper.Map<IssueItemOutputDTO>(issueItem);
        var tags = await _issueTagsRepository.List(
            s => s.Tag,
            x => x.IssueId == issueItem.Id);

        mappedResult.Tags = _mapper.Map<List<TagOutputDTO>>(tags);
        return mappedResult;
    }

    public async Task<Result<PageResult<IssueItemOutputDTO>>> GetPage(IssuesPageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<IssueItemOutputDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<IssueItemOutputDTO>>(result.Data);
            return new PageResult<IssueItemOutputDTO>(mappedData, filter, result.TotalCount);
        },
            Fail: exception => new(exception)
        );
    }

    public async Task<Result<IssueItemOutputDTO>> Create(CreateIssueDTO dto)
    {
        var entity = _mapper.Map<IssueItem>(dto);

        List<IssueTag> issueTags = new();

        foreach (var tagId in dto.TagIds)
            issueTags.Add(new(tagId, 0));

        entity.IssueTags = issueTags;

        var result = await _repository.Create(entity);

        return result.Match(
            Succ: s => GetResult(s).Result,
            Fail: e => new(e)
        );
    }

    public async Task<Result<IssueItemOutputDTO>> Update(UpdateIssueDTO dto)
    {
        var entity = _mapper.Map<IssueItem>(dto);

        List<IssueTag> issueTags = new();

        foreach (var tagId in dto.TagIds)
            issueTags.Add(new(tagId, 0));

        entity.IssueTags = issueTags;

        var result = await _repository.Update(entity);

        return result.Match(
            Succ: s => GetResult(s).Result,
            Fail: e => new(e)
        );
    }

    public async Task<Result<IssueItemOutputDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<IssueItemOutputDTO>>(
            Some: s => _mapper.Map<IssueItemOutputDTO>(s),
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
}
