namespace ConverseSpace.Data.Entities;

public class CommunityTagEntity
{
    public Guid Id { get; set; }

    public Guid Community { get; set; }

    public string Title { get; set; } = null!;

    public bool? Nsfw { get; set; }

    public virtual CommunityEntity CommunityEntityNavigation { get; set; } = null!;

    public virtual ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();
}