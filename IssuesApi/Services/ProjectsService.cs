using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using IssuesApi.Domain.Filter;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class ProjectsService : BaseService, IProjectsService
{
    private readonly IProjectsRepository _repository;
    public ProjectsService(IProjectsRepository repository, IMapper mapper)
        : base(mapper)
    {
        _repository = repository;
    }

    public async Task<Result<ProjectOutputDTO>> Create(CreateProjectDTO dto)
    {
        var entity = _mapper.Map<Project>(dto);
        var result = await _repository.Create(entity);

        return result.Match<Result<ProjectOutputDTO>>(
            Succ: s => _mapper.Map<ProjectOutputDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<ProjectOutputDTO>> Update(UpdateProjectDTO dto)
    {
        var entity = _mapper.Map<Project>(dto);
        var result = await _repository.Update(entity);

        return result.Match<Result<ProjectOutputDTO>>(
            Succ: s => _mapper.Map<ProjectOutputDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<ProjectOutputDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<ProjectOutputDTO>>(
            Some: s => s,
            None: () => new(ResourceNotFoundException.Create(nameof(Project), $"Id == {id}"))
        );
    }

    public async Task<Result<PageResult<ProjectOutputDTO>>> GetPage(ProjectFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<ProjectOutputDTO>>>(
            Succ: (result) => new PageResult<ProjectOutputDTO>(
                result.Data, filter, result.TotalCount),
            Fail: exception => new(exception)
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
