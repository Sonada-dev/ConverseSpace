using AutoMapper;
using ConverseSpace.Application.Authentication.Commands.Login;
using ConverseSpace.Application.Authentication.Commands.Register;
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
    }
}