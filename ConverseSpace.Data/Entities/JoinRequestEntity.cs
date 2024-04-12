using System.ComponentModel.DataAnnotations.Schema;
using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Data.Entities;

public partial class JoinRequestEntity
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid Community { get; set; }

    public DateTime CreatedAt { get; set; }
    
    [Column("status")] 
    public StatusRequest? Status { get; set; }

    public virtual CommunityEntity CommunityNavigation { get; set; } = null!;

    public virtual UserEntity UserNavigation { get; set; } = null!;
}
