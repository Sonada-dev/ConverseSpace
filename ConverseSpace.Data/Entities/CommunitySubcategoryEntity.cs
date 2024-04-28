using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class CommunitySubcategoryEntity
{
    public Guid Id { get; set; }

    public Guid Community { get; set; }

    public Guid Subcategory { get; set; }

    public virtual CommunityEntity CommunityEntityNavigation { get; set; } = null!;

    public virtual SubcategoryEntity SubcategoryEntityNavigation { get; set; } = null!;
}
