namespace ConverseSpace.Data.Entities;

public class CommunityEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public bool? Private { get; set; }

    public bool? CheckPosts { get; set; }

    public virtual ICollection<CommunityTagEntity> CommunityTags { get; set; } = new List<CommunityTagEntity>();

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<UserEntity> Followers { get; set; } = new List<UserEntity>();
}