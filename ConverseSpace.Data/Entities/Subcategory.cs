using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class Subcategory
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public Guid Parent { get; set; }

    public virtual Category ParentNavigation { get; set; } = null!;

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
