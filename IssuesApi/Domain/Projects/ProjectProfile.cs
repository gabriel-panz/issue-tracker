using AutoMapper;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Outputs;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectOutputDTO>().ReverseMap();
    }
}
