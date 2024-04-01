using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class PostDislike
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid Post { get; set; }

    public virtual Post PostNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
