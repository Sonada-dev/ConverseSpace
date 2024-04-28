using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class TagEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Color { get; set; } = null!;

    public virtual ICollection<CommunityTagEntity> CommunityTags { get; set; } = new List<CommunityTagEntity>();

    public virtual ICollection<PostTagEntity> PostTags { get; set; } = new List<PostTagEntity>();
}
