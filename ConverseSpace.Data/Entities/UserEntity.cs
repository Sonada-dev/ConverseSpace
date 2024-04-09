using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class UserEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Description { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public string Avatar { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<CommentDislikeEntity> CommentDislikes { get; set; } = new List<CommentDislikeEntity>();

    public virtual ICollection<CommentLikeEntity> CommentLikes { get; set; } = new List<CommentLikeEntity>();

    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    public virtual ICollection<CommunityEntity> Communities { get; set; } = new List<CommunityEntity>();

    public virtual ICollection<PostDislikeEntity> PostDislikes { get; set; } = new List<PostDislikeEntity>();

    public virtual ICollection<PostLikeEntity> PostLikes { get; set; } = new List<PostLikeEntity>();

    public virtual ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();

    public virtual RoleEntity RoleEntityNavigation { get; set; } = null!;

    public virtual ICollection<CommunityEntity> Communities1 { get; set; } = new List<CommunityEntity>();

    public virtual ICollection<CommunityEntity> CommunitiesNavigation { get; set; } = new List<CommunityEntity>();
}
