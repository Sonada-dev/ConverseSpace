using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Domain.Models;

public class PostContentMedia
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public Guid Post { get; private set; }
    public MediaType Type { get; private set; }
    
    public PostContentMedia(Guid id, string content, Guid post, MediaType type)
    {
        Id = id;
        Content = content;
        Post = post;
        Type = type;
    }
}