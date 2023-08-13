using AutoMapper;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs.Tags;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDTO>()
            .ReverseMap();

        CreateMap<CreateTagDTO, TagDTO>()
            .ReverseMap();

        CreateMap<CreateTagDTO, Tag>()
            .ReverseMap();
    }
}
