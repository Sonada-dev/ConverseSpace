using ConverseSpace.Data.Entities;
using Microsoft.EntityFrameworkCore;
// ReSharper disable All

namespace ConverseSpace.Data;

public partial class CSDBContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public CSDBContext(DbContextOptions<CSDBContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        : base(options)
    {
    }

    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<CommentEntityEntity> Comments { get; set; }

    public virtual DbSet<CommentContentMediaEntity> CommentContentMedia { get; set; }

    public virtual DbSet<CommentDislikeEntity> CommentDislikes { get; set; }

    public virtual DbSet<CommentLikeEntity> CommentLikes { get; set; }

    public virtual DbSet<CommunityEntity> Communities { get; set; }

    public virtual DbSet<CommunityTagEntity> CommunityTags { get; set; }

    public virtual DbSet<PostEntity> Posts { get; set; }

    public virtual DbSet<PostContentMedia> PostContentMedia { get; set; }

    public virtual DbSet<PostDislikeEntity> PostDislikes { get; set; }

    public virtual DbSet<PostLikeEntity> PostLikes { get; set; }

    public virtual DbSet<RoleEntity> Roles { get; set; }

    public virtual DbSet<SubcategoryEntity> Subcategories { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("comments_settings", new[] { "closed", "open", "open_for_followers" })
            .HasPostgresEnum("media_type", new[] { "img", "video", "audio", "gif" })
            .HasPostgresEnum("status_post", new[] { "published", "suggested", "rejected", "deleted" });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<CommentEntityEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ContentText)
                .HasColumnType("character varying")
                .HasColumnName("content_text");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comments_created_by_fkey");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("comments_post_id_fkey");
        });

        modelBuilder.Entity<CommentContentMediaEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_content_media_pkey");

            entity.ToTable("comment_content_media");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Content)
                .HasColumnType("character varying")
                .HasColumnName("content");

            entity.HasOne(d => d.CommentEntityEntityNavigation).WithMany(p => p.CommentContentMedia)
                .HasForeignKey(d => d.Comment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_content_media_comment_fkey");
        });

        modelBuilder.Entity<CommentDislikeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_dislikes_pkey");

            entity.ToTable("comment_dislikes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.CommentEntityEntityNavigation).WithMany(p => p.CommentDislikes)
                .HasForeignKey(d => d.Comment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_dislikes_comment_fkey");

            entity.HasOne(d => d.UserEntityNavigation).WithMany(p => p.CommentDislikes)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_dislikes_user_fkey");
        });

        modelBuilder.Entity<CommentLikeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_likes_pkey");

            entity.ToTable("comment_likes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.CommentEntityEntityNavigation).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.Comment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_likes_comment_fkey");

            entity.HasOne(d => d.UserEntityNavigation).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_likes_user_fkey");
        });

        modelBuilder.Entity<CommunityEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("communities_pkey");

            entity.ToTable("communities");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CheckPosts)
                .HasDefaultValue(false)
                .HasColumnName("check_posts");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Private)
                .HasDefaultValue(false)
                .HasColumnName("private");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Communities)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("communities_created_by_fkey");
        });

        modelBuilder.Entity<CommunityTagEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("community_tags_pkey");

            entity.ToTable("community_tags");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Community).HasColumnName("community");
            entity.Property(e => e.Nsfw)
                .HasDefaultValue(false)
                .HasColumnName("nsfw");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.CommunityEntityNavigation).WithMany(p => p.CommunityTags)
                .HasForeignKey(d => d.Community)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("community_tags_community_fkey");

            entity.HasMany(d => d.Posts).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    r => r.HasOne<PostEntity>().WithMany()
                        .HasForeignKey("Post")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_tags_post_fkey"),
                    l => l.HasOne<CommunityTagEntity>().WithMany()
                        .HasForeignKey("Tag")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_tags_tag_fkey"),
                    j =>
                    {
                        j.HasKey("Tag", "Post").HasName("post_tags_pkey");
                        j.ToTable("post_tags");
                        j.IndexerProperty<Guid>("Tag").HasColumnName("tag");
                        j.IndexerProperty<Guid>("Post").HasColumnName("post");
                    });
        });

        modelBuilder.Entity<PostEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ContentText)
                .HasColumnType("character varying")
                .HasColumnName("content_text");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posts_created_by_fkey");

            entity.HasMany(d => d.Subcategories).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostSubcategory",
                    r => r.HasOne<SubcategoryEntity>().WithMany()
                        .HasForeignKey("Subcategory")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_subcategories_subcategory_fkey"),
                    l => l.HasOne<PostEntity>().WithMany()
                        .HasForeignKey("Post")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("post_subcategories_post_fkey"),
                    j =>
                    {
                        j.HasKey("Post", "Subcategory").HasName("post_subcategories_pkey");
                        j.ToTable("post_subcategories");
                        j.IndexerProperty<Guid>("Post").HasColumnName("post");
                        j.IndexerProperty<Guid>("Subcategory").HasColumnName("subcategory");
                    });
        });

        modelBuilder.Entity<PostContentMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_content_media_pkey");

            entity.ToTable("post_content_media");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("character varying")
                .HasColumnName("content");
            entity.Property(e => e.Post).HasColumnName("post");

            entity.HasOne(d => d.PostEntityNavigation).WithMany(p => p.PostContentMedia)
                .HasForeignKey(d => d.Post)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_content_media_post_fkey");
        });

        modelBuilder.Entity<PostDislikeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_dislikes_pkey");

            entity.ToTable("post_dislikes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.PostEntityNavigation).WithMany(p => p.PostDislikes)
                .HasForeignKey(d => d.Post)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_dislikes_post_fkey");

            entity.HasOne(d => d.UserEntityNavigation).WithMany(p => p.PostDislikes)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_dislikes_user_fkey");
        });

        modelBuilder.Entity<PostLikeEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_likes_pkey");

            entity.ToTable("post_likes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.PostEntityNavigation).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.Post)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_likes_post_fkey");

            entity.HasOne(d => d.UserEntityNavigation).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_likes_user_fkey");
        });

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<SubcategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subcategories_pkey");

            entity.ToTable("subcategories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Parent).HasColumnName("parent");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.ParentNavigation).WithMany(p => p.Subcategories)
                .HasForeignKey(d => d.Parent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subcategories_parent_fkey");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Avatar)
                .HasColumnType("character varying")
                .HasColumnName("avatar");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasColumnType("character varying")
                .HasColumnName("passwordHash");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");

            entity.HasOne(d => d.RoleEntityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_fkey");

            entity.HasMany(d => d.CommunitiesNavigation).WithMany(p => p.Followers)
                .UsingEntity<Dictionary<string, object>>(
                    "Follow",
                    r => r.HasOne<CommunityEntity>().WithMany()
                        .HasForeignKey("Community")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("follows_community_fkey"),
                    l => l.HasOne<UserEntity>().WithMany()
                        .HasForeignKey("Follower")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("follows_follower_fkey"),
                    j =>
                    {
                        j.HasKey("Follower", "Community").HasName("follows_pkey");
                        j.ToTable("follows");
                        j.IndexerProperty<Guid>("Follower").HasColumnName("follower");
                        j.IndexerProperty<Guid>("Community").HasColumnName("community");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}