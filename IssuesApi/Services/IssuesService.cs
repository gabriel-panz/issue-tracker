using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class IssuesService : BaseService<IssueItemDTO, IssueItem>, IIssuesService
{
    private new readonly IIssuesRepository _repository;
    public IssuesService(IIssuesRepository repository, IMapper mapper)
        : base(repository, mapper)
    {
        _repository = repository;
    }

    public async Task<Result<PageResult<IssueItemDTO>>> GetPage(long projectId, PageFilter filter)
    {
        var result = await _repository.GetPage(projectId, filter);

        return result.Match<Result<PageResult<IssueItemDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<IssueItemDTO>>(result.data);
            return new PageResult<IssueItemDTO>(mappedData, filter, result.total);
        },
            Fail: exception => new(exception)
        );
    }
}
