using AutoMapper;
using Core.Entities;
using DTOs.OperationClaims;
using DTOs.Users;

namespace Buisness.Mapping.Automapper;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        // User Mapping 
        CreateMap<User, UserListDto>().ReverseMap();


        CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();


        CreateMap<User, UserForOperationClaimDto>().ReverseMap();

        CreateMap<UserForRegisterDto, UserForOperationClaimDto>().ReverseMap();
    }
}