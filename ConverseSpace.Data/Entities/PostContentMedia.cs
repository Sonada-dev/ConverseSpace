using System;
using System.Collections.Generic;
using ConverseSpace.Data.Entities.Enums;

namespace ConverseSpace.Data.Entities;

public partial class PostContentMedia
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;
    
    public MediaType Type { get; set; }

    public Guid Post { get; set; }

    public virtual Post PostNavigation { get; set; } = null!;
}
