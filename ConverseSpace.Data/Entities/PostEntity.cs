using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class PostEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ContentText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PostContentMediaEntity> PostContentMedia { get; set; } = new List<PostContentMediaEntity>();

    public virtual ICollection<PostDislikeEntity> PostDislikes { get; set; } = new List<PostDislikeEntity>();

    public virtual ICollection<PostLikeEntity> PostLikes { get; set; } = new List<PostLikeEntity>();

    public virtual ICollection<SubcategoryEntity> Subcategories { get; set; } = new List<SubcategoryEntity>();

    public virtual ICollection<CommunityTagEntity> Tags { get; set; } = new List<CommunityTagEntity>();
}
