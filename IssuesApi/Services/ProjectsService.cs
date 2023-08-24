using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs.Projects;
using IssuesApi.Domain.Outputs.Projects;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class ProjectsService : BaseService<ProjectDTO, Project>, IProjectsService
{
    private readonly IProjectsRepository _repository;
    public ProjectsService(IProjectsRepository repository, IMapper mapper)
        : base(mapper)
    {
        _repository = repository;
    }

    public async Task<Result<ProjectDTO>> Create(CreateProjectDTO dto)
    {
        var entity = _mapper.Map<Project>(dto);
        var result = await _repository.Create(entity);

        return result.Match<Result<ProjectDTO>>(
            Succ: s => _mapper.Map<ProjectDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<ProjectDTO>> Update(UpdateProjectDTO dto)
    {
        var entity = _mapper.Map<Project>(dto);
        var result = await _repository.Update(entity);

        return result.Match<Result<ProjectDTO>>(
            Succ: s => _mapper.Map<ProjectDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<ProjectOutputDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<ProjectOutputDTO>>(
            Some: s => s,
            None: () => new(new ResourceNotFoundException())
        );
    }

    public async Task<Result<PageResult<ProjectDTO>>> GetPage(PageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<ProjectDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<ProjectDTO>>(result.Data);
            return new PageResult<ProjectDTO>(mappedData, filter, result.TotalCount);
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
        return await _repository.SoftDelete(id);
    }
}
