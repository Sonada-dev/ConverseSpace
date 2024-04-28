using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Domain.Models;

public class Community
{
    public Community() { }
    
    public Community(string title,
        string? description,
        Guid createdBy,
        bool isPrivate = false,
        bool checkPosts = false,
        CommentsSettings commentsSettings = CommentsSettings.Open)
    {
        Title = title;
        Description = description;
        CreatedBy = createdBy;
        Private = isPrivate;
        CheckPosts = checkPosts;
        Comments = commentsSettings;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public DateOnly CreatedAt { get; private set; } = DateOnly.FromDateTime(DateTime.Today);
    public Guid CreatedBy { get; private set; }
    public bool Private { get; private set; }
    public bool CheckPosts { get; private set; }
    public bool IsDeleted { get; set; }
    
    public int FollowersCount { get; private set; } = 0;
    public CommentsSettings Comments { get; private set; }
    public ICollection<CommunityTag> Tags { get; private set; } = new List<CommunityTag>();
    public ICollection<Follow> Follows { get; private set; } = new List<Follow>();
    public ICollection<JoinRequest> JoinRequests { get; private set; } = new List<JoinRequest>();

    
    
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
    
    public void UpdatePrivate(bool isPrivate)
    {
        Private = isPrivate;
    }
    
    public void Delete(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }
}
