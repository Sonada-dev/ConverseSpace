﻿namespace ConverseSpace.Data.Entities;

public class PostDislike
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid Post { get; set; }

    public virtual PostEntity PostEntityNavigation { get; set; } = null!;

    public virtual UserEntity UserEntityNavigation { get; set; } = null!;
}