using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Data.Entities;

public partial class PostContentMediaEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public Guid Post { get; set; }
    
    [Column("type")]
    public MediaType Type { get; set; }

    public virtual PostEntity PostEntityNavigation { get; set; } = null!;
}
