using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
