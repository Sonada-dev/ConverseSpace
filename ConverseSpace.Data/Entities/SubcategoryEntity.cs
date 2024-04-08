using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class SubcategoryEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public Guid Parent { get; set; }

    public virtual CategoryEntity ParentNavigation { get; set; } = null!;

    public virtual ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
}
