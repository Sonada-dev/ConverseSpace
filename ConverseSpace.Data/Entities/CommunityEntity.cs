using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ConverseSpace.Domain.Models.Enums;

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
    
    [Column("comments")] 
    public CommentsSettings Comments { get; set; }
    
    public bool? IsDeleted { get; set; }

    public virtual ICollection<CommunityTagEntity> CommunityTags { get; set; } = new List<CommunityTagEntity>();

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<UserEntity> Followers { get; set; } = new List<UserEntity>();

    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    
    public virtual ICollection<JoinRequestEntity> JoinRequests { get; set; } = new List<JoinRequestEntity>();
}
