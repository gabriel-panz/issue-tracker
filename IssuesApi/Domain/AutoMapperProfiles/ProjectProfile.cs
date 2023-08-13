using AutoMapper;
using IssuesApi.Domain.DTOs;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs.Projects;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectDTO>()
            .ReverseMap();

        CreateMap<CreateProjectDTO, ProjectDTO>()
            .ReverseMap();
    }
}
