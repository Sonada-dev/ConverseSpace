namespace ConverseSpace.Data.Entities;

public class CommentLikeEntity
{
    public Guid Id { get; set; }

    public Guid User { get; set; }

    public Guid Comment { get; set; }

    public virtual CommentEntityEntity CommentEntityEntityNavigation { get; set; } = null!;

    public virtual UserEntity UserEntityNavigation { get; set; } = null!;
}