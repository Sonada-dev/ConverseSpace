namespace ConverseSpace.Data.Entities;

public class PostEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? ContentText { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual ICollection<CommentEntityEntity> Comments { get; set; } = new List<CommentEntityEntity>();

    public virtual UserEntity CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PostContentMedia> PostContentMedia { get; set; } = new List<PostContentMedia>();

    public virtual ICollection<PostDislikeEntity> PostDislikes { get; set; } = new List<PostDislikeEntity>();

    public virtual ICollection<PostLikeEntity> PostLikes { get; set; } = new List<PostLikeEntity>();

    public virtual ICollection<SubcategoryEntity> Subcategories { get; set; } = new List<SubcategoryEntity>();

    public virtual ICollection<CommunityTagEntity> Tags { get; set; } = new List<CommunityTagEntity>();
}