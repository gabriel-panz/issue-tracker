using AutoMapper;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagOutputDTO>().ReverseMap();
    }
}
