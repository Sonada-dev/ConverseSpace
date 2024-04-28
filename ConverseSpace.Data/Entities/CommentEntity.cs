using System;
using System.Collections.Generic;
using ConverseSpace.Domain.Models.Enums;
using NpgsqlTypes;

namespace ConverseSpace.Data.Entities;

public partial class CommentEntity
{
    public Guid Id { get; set; }

    public Guid? PostId { get; set; }

    public string? ContentText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }
    
    [PgName("comments")]
    public CommentsSettings Comments {get; set;}
    

    public bool? IsDeleted { get; set; }

    public int LikesCount { get; set; }

    public int DislikesCount { get; set; }

    public virtual ICollection<CommentContentMediaEntity> CommentContentMedia { get; set; } = new List<CommentContentMediaEntity>();

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual PostEntity? Post { get; set; }

    public virtual ICollection<ReactionEntity> Reactions { get; set; } = new List<ReactionEntity>();
}
