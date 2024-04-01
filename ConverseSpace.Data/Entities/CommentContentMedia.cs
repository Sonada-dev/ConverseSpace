using System;
using System.Collections.Generic;
using ConverseSpace.Data.Entities.Enums;

namespace ConverseSpace.Data.Entities;

public partial class CommentContentMedia
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;
    
    public MediaType Type { get; set; }

    public Guid Comment { get; set; }

    public virtual Comment CommentNavigation { get; set; } = null!;
}
