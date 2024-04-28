﻿using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class CommunityTagEntity
{
    public Guid Id { get; set; }

    public Guid Community { get; set; }

    public Guid Tag { get; set; }

    public virtual CommunityEntity CommunityEntityNavigation { get; set; } = null!;

    public virtual TagEntity TagEntityNavigation { get; set; } = null!;
}
