using System;
using System.Collections.Generic;
using ConverseSpace.Domain.Models.Enums;
using NpgsqlTypes;

namespace ConverseSpace.Data.Entities;

public partial class PostContentMediaEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;
    
    [PgName("type")]
    public MediaType Type { get; set; }

    public Guid Post { get; set; }

    public virtual PostEntity PostEntityNavigation { get; set; } = null!;
}
