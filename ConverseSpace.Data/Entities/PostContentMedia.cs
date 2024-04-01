﻿using ConverseSpace.Data.Entities.Enums;

namespace ConverseSpace.Data.Entities;

public class PostContentMedia
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public MediaType Type { get; set; }

    public Guid Post { get; set; }

    public virtual PostEntity PostEntityNavigation { get; set; } = null!;
}