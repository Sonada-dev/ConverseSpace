using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class Post
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ContentText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PostContentMedia> PostContentMedia { get; set; } = new List<PostContentMedia>();

    public virtual ICollection<PostDislike> PostDislikes { get; set; } = new List<PostDislike>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

    public virtual ICollection<CommunityTag> Tags { get; set; } = new List<CommunityTag>();
}
