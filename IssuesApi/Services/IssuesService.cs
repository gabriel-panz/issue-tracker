using AutoMapper;
using IssuesApi.Classes.Base;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;

namespace IssuesApi.Services;

public class IssuesService : BaseService<IssueItemDTO, IssueItem>, IIssuesService
{
    public IssuesService(IIssuesRepository repository, IMapper mapper)
        : base(repository, mapper)
    {
    }
}
