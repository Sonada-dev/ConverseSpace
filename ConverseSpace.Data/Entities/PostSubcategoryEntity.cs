using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class PostSubcategoryEntity
{
    public Guid Id { get; set; }

    public Guid Post { get; set; }

    public Guid Subcategory { get; set; }

    public virtual PostEntity PostEntityNavigation { get; set; } = null!;

    public virtual SubcategoryEntity SubcategoryEntityNavigation { get; set; } = null!;
}
