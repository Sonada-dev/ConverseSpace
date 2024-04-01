using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class Community
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public bool? Private { get; set; }

    public bool? CheckPosts { get; set; }

    public virtual ICollection<CommunityTag> CommunityTags { get; set; } = new List<CommunityTag>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<User> Followers { get; set; } = new List<User>();
}
