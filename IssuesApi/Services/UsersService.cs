using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Classes.Pagination;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Filters;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using BC = BCrypt.Net;
using LanguageExt.Common;

namespace IssuesApi.Services;

public class UsersService : BaseService
    , IUsersService
{
    private readonly IUsersRepository _repository;
    public UsersService(
        IUsersRepository repository,
        IMapper mapper)
            : base(mapper)
    {
        _repository = repository;
    }

    private Result<UserOutputDTO> GetResult(User user)
    {
        return _mapper.Map<UserOutputDTO>(user);
    }

    public async Task<Result<PageResult<UserOutputDTO>>> GetPage(UsersPageFilter filter)
    {
        var result = await _repository.GetPage(filter);

        return result.Match<Result<PageResult<UserOutputDTO>>>(
            Succ: (result) =>
        {
            var mappedData = _mapper.Map<List<UserOutputDTO>>(result.Data);
            return new PageResult<UserOutputDTO>(mappedData, filter, result.TotalCount);
        },
            Fail: exception => new(exception)
        );
    }

    public async Task<Result<UserOutputDTO>> Create(CreateUserDTO dto)
    {
        var entity = _mapper.Map<User>(dto);

        entity.Password = BC.BCrypt.HashPassword(dto.Password);

        var result = await _repository.Create(entity);

        return result.Match(
            Succ: s => GetResult(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<UserOutputDTO>> Update(UpdateUserDTO dto)
    {
        var entity = _mapper.Map<User>(dto);

        var result = await _repository.Update(entity);

        return result.Match(
            Succ: s => GetResult(s),
            Fail: e => new(e)
        );
    }

    public async Task<Result<UserOutputDTO>> Get(long id)
    {
        var result = await _repository.Get(id);

        return result.Match<Result<UserOutputDTO>>(
            Some: s => _mapper.Map<UserOutputDTO>(s),
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
