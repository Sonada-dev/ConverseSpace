using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class CommunityEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public bool? Private { get; set; }

    public bool? CheckPosts { get; set; }

    public bool? IsDeleted { get; set; }
    
    public int FollowersCount { get; set; }
    
    public virtual ICollection<CommunitySubcategoryEntity> CommunitySubcategories { get; set; } = new List<CommunitySubcategoryEntity>();

    public virtual ICollection<CommunityTagEntity> CommunityTags { get; set; } = new List<CommunityTagEntity>();

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<FollowEntity> Follows { get; set; } = new List<FollowEntity>();

    public virtual ICollection<JoinRequestEntity> JoinRequests { get; set; } = new List<JoinRequestEntity>();

    public virtual ICollection<ModeratorEntity> Moderators { get; set; } = new List<ModeratorEntity>();

    public virtual ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
}
