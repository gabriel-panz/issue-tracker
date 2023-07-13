using AutoMapper;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDTO>()
            .ReverseMap();
    }
}
