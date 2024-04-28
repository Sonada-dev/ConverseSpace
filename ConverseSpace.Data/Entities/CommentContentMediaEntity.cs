using System;
using System.Collections.Generic;
using ConverseSpace.Domain.Models.Enums;
using NpgsqlTypes;

namespace ConverseSpace.Data.Entities;

public partial class CommentContentMediaEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;
    
    [PgName("type")]
    public MediaType Type { get; set; }

    public Guid Comment { get; set; }

    public virtual CommentEntity CommentEntityNavigation { get; set; } = null!;
}
