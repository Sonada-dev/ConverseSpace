using System;
using System.Collections.Generic;
using ConverseSpace.Domain.Models.Enums;
using NpgsqlTypes;

namespace ConverseSpace.Data.Entities;

public partial class PostEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ContentText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }
    
    [PgName("type")]
    public StatusPost Type { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid Community { get; set; }

    public int LikesCount { get; set; }

    public int DislikesCount { get; set; }

    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    public virtual CommunityEntity CommunityEntityNavigation { get; set; } = null!;

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PostContentMediaEntity> PostContentMedia { get; set; } = new List<PostContentMediaEntity>();

    public virtual ICollection<PostSubcategoryEntity> PostSubcategories { get; set; } = new List<PostSubcategoryEntity>();

    public virtual ICollection<PostTagEntity> PostTags { get; set; } = new List<PostTagEntity>();

    public virtual ICollection<ReactionEntity> Reactions { get; set; } = new List<ReactionEntity>();
}
