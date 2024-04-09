using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class CommentDislikeEntity
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid Comment { get; set; }

    public virtual CommentEntity CommentEntityNavigation { get; set; } = null!;

    public virtual UserEntity UserEntityNavigation { get; set; } = null!;
}
