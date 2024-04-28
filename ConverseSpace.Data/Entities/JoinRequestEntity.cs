﻿using System;
using System.Collections.Generic;
using ConverseSpace.Domain.Models.Enums;
using NpgsqlTypes;

namespace ConverseSpace.Data.Entities;

public partial class JoinRequestEntity
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid Community { get; set; }

    public DateTime CreatedAt { get; set; }
    
    [PgName("type")]
    public StatusRequest Type { get; set; }

    public virtual CommunityEntity CommunityEntityNavigation { get; set; } = null!;

    public virtual UserEntity UserEntityNavigation { get; set; } = null!;
}
