using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class ModeratorEntity
{
    public Guid Community { get; set; }

    public Guid User { get; set; }

    public Guid Id { get; set; }

    public virtual CommunityEntity CommunityEntityNavigation { get; set; } = null!;

    public virtual UserEntity UserEntityNavigation { get; set; } = null!;
}
