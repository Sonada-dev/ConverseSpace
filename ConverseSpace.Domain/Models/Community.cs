using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Domain.Models;

public class Community
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public DateOnly CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public bool IsPrivate { get; private set; }
    public bool CheckPosts { get; private set; }
    public CommentsSettings Comments { get; private set; }
    public ICollection<CommunityTag> Tags { get; private set; } = null!;
    public ICollection<User> Followers { get; private set; } = null!;

    private Community()
    {
        // Private constructor for Entity Framework or other ORM
    }

    public Community(string title,
        string? description,
        Guid createdBy,
        bool isPrivate = false,
        bool checkPosts = false,
        CommentsSettings commentsSettings = CommentsSettings.open)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        CreatedAt = DateOnly.FromDateTime(DateTime.Today);
        CreatedBy = createdBy;
        IsPrivate = isPrivate;
        CheckPosts = checkPosts;
        Comments = commentsSettings;
        Tags = new List<CommunityTag>();
        Followers = new List<User>();
    }

    public void UpdateTitle(string newTitle)
    {
        Title = newTitle;
    }

    // Добавление тега к сообществу
    public void AddTag(CommunityTag tag)
    {
        if (!Tags.Contains(tag))
            Tags.Add(tag);
    }

    // Удаление тега из сообщества
    public void RemoveTag(CommunityTag tag)
    {
        Tags.Remove(tag);
    }

    // Добавление подписчика
    public void AddFollower(User follower)
    {
        if (!Followers.Contains(follower))
            Followers.Add(follower);
    }

    // Удаление подписчика
    public void RemoveFollower(User follower)
    {
        Followers.Remove(follower);
    }

    // Обновление описания сообщества
    public void UpdateDescription(string? newDescription)
    {
        Description = newDescription;
    }

    // Обновление настроек комментариев
    public void UpdateCommentsSettings(CommentsSettings newSettings)
    {
        Comments = newSettings;
    }
    
}
