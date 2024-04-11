using ConverseSpace.Domain.Errors;
using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.Domain.Models;

public class Community
{
    public Community()
    {
        
    }
    
    public Community(string title,
        string? description,
        Guid createdBy,
        bool isPrivate = false,
        bool checkPosts = false,
        CommentsSettings commentsSettings = CommentsSettings.open)
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
    public CommentsSettings Comments { get; private set; }
    public ICollection<CommunityTag> Tags { get; private set; } = new List<CommunityTag>();
    public ICollection<User> Followers { get; private set; } = new List<User>();
    
    
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
    public Result AddFollower(User follower)
    {
        if (Followers.Any(c => c.Id == follower.Id)) 
            return Result.Failure(FollowsErrors.AlreadyFollowing);
        
        Followers.Add(follower);
        return Result.Success();

    }

    // Удаление подписчика
    public Result RemoveFollower(Guid followerId)
    {
        var follower = Followers.FirstOrDefault(c => c.Id == followerId);
        if (follower == null) 
            return Result.Failure(FollowsErrors.AlreadyUnfollowing);
        
        Followers.Remove(follower);
        return Result.Success();

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
}
