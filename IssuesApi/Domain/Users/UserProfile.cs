using AutoMapper;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Inputs;
using IssuesApi.Domain.Outputs;

namespace IssuesApi.Domain.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserOutputDTO>().ReverseMap();
        CreateMap<CreateUserDTO, User>();
        CreateMap<UpdateUserDTO, User>();
    }
}
