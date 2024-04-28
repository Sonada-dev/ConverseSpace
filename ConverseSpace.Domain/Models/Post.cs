using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Domain.Models;

public class Post
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = null!;
    public string? ContentText { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public Guid CreatedBy { get; private set; }
    public Guid Community { get; set; }
    public StatusPost Status { get; private set; }
    public bool IsDeleted { get; private set; }
    public ICollection<PostContentMedia> PostContentMedia { get; private set; } = new List<PostContentMedia>();

    protected Post() {}
    
    public Post(string title,
        string? contentText,
        Guid createdBy,
        StatusPost status = StatusPost.Published)
    {
        Title = title;
        ContentText = contentText;
        CreatedBy = createdBy;
        Status = status;
    }

    public void UpdateContentMedia(List<PostContentMedia> mediaContent)
    {
        PostContentMedia = mediaContent;
    }
}