using AutoMapper;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Infrastructure.Configuration;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserEntity>();
        CreateMap<UserEntity, User>();
        
        CreateMap<Role, RoleEntity>();
        CreateMap<RoleEntity, Role>();
        
    }
}