using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class CommunityTag
{
    public Guid Id { get; set; }

    public Guid Community { get; set; }

    public string Title { get; set; } = null!;

    public bool? Nsfw { get; set; }

    public virtual Community CommunityNavigation { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
