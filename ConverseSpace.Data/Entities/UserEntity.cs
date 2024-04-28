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

    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    public virtual ICollection<CommunityEntity> Communities { get; set; } = new List<CommunityEntity>();

    public virtual ICollection<FollowEntity> Follows { get; set; } = new List<FollowEntity>();

    public virtual ICollection<JoinRequestEntity> JoinRequests { get; set; } = new List<JoinRequestEntity>();

    public virtual ICollection<ModeratorEntity> Moderators { get; set; } = new List<ModeratorEntity>();

    public virtual ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();

    public virtual ICollection<ReactionEntity> Reactions { get; set; } = new List<ReactionEntity>();

    public virtual RoleEntity RoleEntityNavigation { get; set; } = null!;
}
