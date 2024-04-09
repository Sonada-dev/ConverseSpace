namespace ConverseSpace.Domain.Models;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; }
    public string? Description { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateOnly CreatedAt { get; private set; }
    public string Avatar { get; private set; }
    public int Role { get; private set; }
    //public ICollection<Comment> Comments { get; private set; }
    //public ICollection<Post> Posts { get; private set; }
    public ICollection<Community> Communities { get; private set; }
    

    // Конструктор для создания нового пользователя
    public User(string username, string email, string passwordHash, string? description = null, int? role = 3)
    {
        Id = Guid.NewGuid();
        Username = username;
        Description = description;
        Email = email;
        PasswordHash = passwordHash;
        Role = role!.Value;
        CreatedAt = DateOnly.FromDateTime(DateTime.Today);
        Avatar = string.Empty; // Можно установить значение по умолчанию или оставить null
        //Comments = new List<Comment>();
        //Posts = new List<Post>();
        Communities = [];
    }


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