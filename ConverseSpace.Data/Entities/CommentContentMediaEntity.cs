using ConverseSpace.Data.Entities.Enums;
// ReSharper disable All

namespace ConverseSpace.Data.Entities;

public class CommentContentMediaEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = null!;

    public MediaType Type { get; set; }

    public Guid Comment { get; set; }

    public virtual CommentEntityEntity CommentEntityEntityNavigation { get; set; } = null!;
}