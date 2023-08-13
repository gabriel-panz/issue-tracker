using AutoMapper;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs.Issues;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class IssueItemProfile : Profile
{
    public IssueItemProfile()
    {
        CreateMap<IssueItem, IssueItemDTO>()
            .ReverseMap();

        CreateMap<CreateIssueDTO, IssueItemDTO>()
            .ReverseMap();
    }
}
