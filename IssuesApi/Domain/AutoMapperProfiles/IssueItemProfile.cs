using AutoMapper;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class IssueItemProfile : Profile
{
    public IssueItemProfile()
    {
        CreateMap<IssueItem, IssueItemDTO>()
            .ReverseMap();
    }
}
