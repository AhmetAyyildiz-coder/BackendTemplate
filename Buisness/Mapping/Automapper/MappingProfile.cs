using AutoMapper;
using Core.Entities;
using DTOs.Users;

namespace Buisness.Mapping.Automapper;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        // User Mapping 
        CreateMap<User, UserListDto>().ReverseMap();

    }
}