using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class PostTagEntity
{
    public Guid Id { get; set; }

    public Guid Post { get; set; }

    public Guid Tag { get; set; }

    public virtual PostEntity PostEntityNavigation { get; set; } = null!;

    public virtual TagEntity TagEntityNavigation { get; set; } = null!;
}
