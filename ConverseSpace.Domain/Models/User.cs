namespace ConverseSpace.Domain.Models;

public class User(string username, string email, string passwordHash, string? description = null, int? role = 3)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = username;
    public string? Description { get; private set; } = description;
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
    public DateOnly CreatedAt { get; private set; } = DateOnly.FromDateTime(DateTime.Today);
    public string Avatar { get; private set; } = string.Empty; // Можно установить значение по умолчанию или оставить null
    public int Role { get; private set; } = role!.Value;

    //public ICollection<Comment> Comments { get; private set; }
    //public ICollection<Post> Posts { get; private set; }
    public ICollection<Community> Communities { get; private set; } = new List<Community>();

    //Comments = new List<Comment>();
    //Posts = new List<Post>();


    // Метод для добавления комментария
    // public void AddComment(Comment comment)
    // {
    //     Comments.Add(comment);
    // }

    // Метод для добавления сообщества
    public void AddCommunity(Community community)
    {
        Communities.Add(community);
    }

    // Метод для добавления поста
    // public void AddPost(Post post)
    // {
    //     Posts.Add(post);
    // }
}