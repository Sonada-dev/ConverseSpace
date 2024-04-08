using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class PostContentMediaEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public Guid Post { get; set; }

    public virtual PostEntity PostEntityNavigation { get; set; } = null!;
}
