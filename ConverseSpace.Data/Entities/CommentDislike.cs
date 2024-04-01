using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class CommentDislike
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid Comment { get; set; }

    public virtual Comment CommentNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
