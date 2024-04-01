using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class Comment
{
    public Guid Id { get; set; }

    public Guid? PostId { get; set; }

    public string? ContentText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual ICollection<CommentContentMedia> CommentContentMedia { get; set; } = new List<CommentContentMedia>();

    public virtual ICollection<CommentDislike> CommentDislikes { get; set; } = new List<CommentDislike>();

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Post? Post { get; set; }
}
