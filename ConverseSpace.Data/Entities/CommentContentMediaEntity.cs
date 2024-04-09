using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class CommentContentMediaEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public Guid Comment { get; set; }

    public virtual CommentEntity CommentEntityNavigation { get; set; } = null!;
}
