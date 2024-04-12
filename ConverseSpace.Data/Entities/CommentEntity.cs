using System;
using System.Collections.Generic;
using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Data.Entities;

public partial class CommentEntity
{
    public Guid Id { get; set; }

    public Guid? PostId { get; set; }

    public string? ContentText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }
    
    public bool? IsDeleted { get; set; }

    public virtual ICollection<CommentContentMediaEntity> CommentContentMedia { get; set; } = new List<CommentContentMediaEntity>();

    public virtual ICollection<CommentDislikeEntity> CommentDislikes { get; set; } = new List<CommentDislikeEntity>();

    public virtual ICollection<CommentLikeEntity> CommentLikes { get; set; } = new List<CommentLikeEntity>();

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual PostEntity? Post { get; set; }
}
