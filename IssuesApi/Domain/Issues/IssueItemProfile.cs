using AutoMapper;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class IssueItemProfile : Profile
{
    public IssueItemProfile()
    {
        CreateMap<IssueItem, IssueItemOutputDTO>().ReverseMap();
    }
}
