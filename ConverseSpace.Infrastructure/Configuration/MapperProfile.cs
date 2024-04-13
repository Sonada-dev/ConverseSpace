using AutoMapper;
using ConverseSpace.Application.Communities.Commands.CreateCommunity;
using ConverseSpace.Application.Communities.Commands.UpdateCommunity;
using ConverseSpace.Application.Communities.Queries.GetCommunities;
using ConverseSpace.Application.Posts.Commands;
using ConverseSpace.Application.Posts.Queries;
using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Models;

namespace ConverseSpace.Infrastructure.Configuration;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserEntity>();
        CreateMap<UserEntity, User>();
        CreateMap<User, Follower>();


        CreateMap<Role, RoleEntity>();
        CreateMap<RoleEntity, Role>();

        CreateMap<Community, CommunityEntity>();
        CreateMap<CommunityEntity, Community>();
        CreateMap<Community, GetCommunityResponse>();
        CreateMap<CreateCommunityRequest, Community>();
        CreateMap<UpdateCommunityRequest, Community>();

        CreateMap<JoinRequest, JoinRequestEntity>();
        CreateMap<JoinRequestEntity, JoinRequest>();

        CreateMap<Post, PostEntity>();
        CreateMap<PostEntity, Post>();
        CreateMap<CreatePostCommand, Post>();
        CreateMap<PostContentMedia, PostContentMediaEntity>();
        CreateMap<PostContentMediaEntity, PostContentMedia>();
        CreateMap<Post, GetPostsResponse>();
    }
}