using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class Category
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
}
