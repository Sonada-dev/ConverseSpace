using System;
using System.Collections.Generic;

namespace ConverseSpace.Data.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Description { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public Guid Role { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string Avatar { get; set; } = null!;

    public virtual ICollection<CommentDislike> CommentDislikes { get; set; } = new List<CommentDislike>();

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Community> Communities { get; set; } = new List<Community>();

    public virtual ICollection<PostDislike> PostDislikes { get; set; } = new List<PostDislike>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Community> CommunitiesNavigation { get; set; } = new List<Community>();
}
