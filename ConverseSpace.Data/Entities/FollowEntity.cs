using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class FollowEntity
{
    public Guid Follower { get; set; }

    public Guid Community { get; set; }

    public Guid Id { get; set; }

    public virtual CommunityEntity CommunityEntityNavigation { get; set; } = null!;

    public virtual UserEntity FollowerNavigation { get; set; } = null!;
}
