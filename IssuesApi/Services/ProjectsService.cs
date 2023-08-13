using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs.Projects;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class ProjectsService : BaseService<ProjectDTO, Project>, IProjectsService
{
    public ProjectsService(IProjectsRepository repository, IMapper mapper)
        : base(repository, mapper)
    {
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

    public async Task<Result<ProjectDTO>> Update(long id, CreateProjectDTO dto)
    {
        var entity = _mapper.Map<Project>(dto);
        var result = await _repository.Update(id, entity);

        return result.Match<Result<ProjectDTO>>(
            Succ: s => _mapper.Map<ProjectDTO>(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<ProjectDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<ProjectDTO>>(
            Some: s => _mapper.Map<ProjectDTO>(s),
            None: () => new(new ResourceNotFoundException())
        );
    }

    public async Task<Result<PageResult<ProjectDTO>>> GetPage(PageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<ProjectDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<ProjectDTO>>(result.data);
            return new PageResult<ProjectDTO>(mappedData, filter, result.total);
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
            await _repository.HardDelete((Project)option);

        return new(true);
    }

    public async Task<Result<bool>> SoftDelete(long id)
    {
        var option = await _repository.Get(id);

        if (option.IsNone)
            return new(new ResourceNotFoundException());
        if (option.IsSome)
            await _repository.SoftDelete((Project)option);

        return new(true);
    }
}
