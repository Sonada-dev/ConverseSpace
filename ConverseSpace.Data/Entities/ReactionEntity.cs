using System;
using System.Collections.Generic;
using ConverseSpace.Domain.Models.Enums;
using NpgsqlTypes;

namespace ConverseSpace.Data.Entities;

public partial class ReactionEntity
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid? Post { get; set; }

    public Guid? Comment { get; set; }
    
    [PgName("type")]
    public ReactType Type { get; set; }

    public virtual CommentEntity? CommentNavigation { get; set; }

    public virtual PostEntity? PostNavigation { get; set; }

    public virtual UserEntity UserEntityNavigation { get; set; } = null!;
}
