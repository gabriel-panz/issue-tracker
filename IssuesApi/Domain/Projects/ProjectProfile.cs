using AutoMapper;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectOutputDTO>().ReverseMap();
        CreateMap<CreateProjectDTO, Project>();
        CreateMap<UpdateProjectDTO, Project>();
    }
}
