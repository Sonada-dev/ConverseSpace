using ConverseSpace.Data.Entities;
using ConverseSpace.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
#pragma warning disable CS0618 // Type or member is obsolete

namespace ConverseSpace.Data;

// ReSharper disable once InconsistentNaming
public partial class CSDBContext : DbContext
{
    public CSDBContext(DbContextOptions<CSDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoryEntity> Categories { get; set; }

    public virtual DbSet<CommentEntity> Comments { get; set; }

    public virtual DbSet<CommentContentMediaEntity> CommentContentMedia { get; set; }

    public virtual DbSet<CommunityEntity> Communities { get; set; }

    public virtual DbSet<CommunitySubcategoryEntity> CommunitySubcategories { get; set; }

    public virtual DbSet<CommunityTagEntity> CommunityTags { get; set; }

    public virtual DbSet<FollowEntity> Follows { get; set; }

    public virtual DbSet<JoinRequestEntity> JoinRequests { get; set; }

    public virtual DbSet<ModeratorEntity> Moderators { get; set; }

    public virtual DbSet<PostEntity> Posts { get; set; }

    public virtual DbSet<PostContentMediaEntity> PostContentMedia { get; set; }

    public virtual DbSet<PostSubcategoryEntity> PostSubcategories { get; set; }

    public virtual DbSet<PostTagEntity> PostTags { get; set; }

    public virtual DbSet<ReactionEntity> Reactions { get; set; }

    public virtual DbSet<RoleEntity> Roles { get; set; }

    public virtual DbSet<SubcategoryEntity> Subcategories { get; set; }

    public virtual DbSet<TagEntity> Tags { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<CommentsSettings>();
        NpgsqlConnection.GlobalTypeMapper.MapComposite<CommunityEntity>();
        
        NpgsqlConnection.GlobalTypeMapper.MapEnum<StatusRequest>();
        NpgsqlConnection.GlobalTypeMapper.MapComposite<JoinRequestEntity>();
        
        NpgsqlConnection.GlobalTypeMapper.MapEnum<StatusPost>();
        NpgsqlConnection.GlobalTypeMapper.MapComposite<PostEntity>();
        
        NpgsqlConnection.GlobalTypeMapper.MapEnum<MediaType>();
        NpgsqlConnection.GlobalTypeMapper.MapComposite<PostContentMediaEntity>();
        
        NpgsqlConnection.GlobalTypeMapper.MapEnum<MediaType>();
        NpgsqlConnection.GlobalTypeMapper.MapComposite<CommentContentMediaEntity>();
        
        NpgsqlConnection.GlobalTypeMapper.MapEnum<ReactType>();
        NpgsqlConnection.GlobalTypeMapper.MapComposite<ReactionEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("comments_settings", new[] { "closed", "open", "open_for_followers" })
            .HasPostgresEnum("media_type", new[] { "img", "video", "audio", "gif" })
            .HasPostgresEnum("react_type", new[] { "like", "dislike" })
            .HasPostgresEnum("status_post", new[] { "published", "suggested", "rejected", "deleted" })
            .HasPostgresEnum("status_request", new[] { "pending", "approved", "rejected" });

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

        modelBuilder.Entity<CommentEntity>(entity =>
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
            entity.Property(e => e.DislikesCount)
                .HasDefaultValue(0)
                .HasColumnName("dislikes_count");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LikesCount)
                .HasDefaultValue(0)
                .HasColumnName("likes_count");
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

            entity.HasOne(d => d.CommentEntityNavigation).WithMany(p => p.CommentContentMedia)
                .HasForeignKey(d => d.Comment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("comment_content_media_comment_fkey");
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
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Private)
                .HasDefaultValue(false)
                .HasColumnName("private");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
            entity.Property(e => e.FollowersCount)
                .HasDefaultValue(0)
                .HasColumnName("followers_count");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Communities)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("communities_created_by_fkey");
        });

        modelBuilder.Entity<CommunitySubcategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("community_subcategories_pk");

            entity.ToTable("community_subcategories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Community).HasColumnName("community");
            entity.Property(e => e.Subcategory).HasColumnName("subcategory");

            entity.HasOne(d => d.CommunityEntityNavigation).WithMany(p => p.CommunitySubcategories)
                .HasForeignKey(d => d.Community)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("community_subcategories_community_id_fk");

            entity.HasOne(d => d.SubcategoryEntityNavigation).WithMany(p => p.CommunitySubcategories)
                .HasForeignKey(d => d.Subcategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("community_subcategories_subcategories_id_fk");
        });

        modelBuilder.Entity<CommunityTagEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("community_tags_pk");

            entity.ToTable("community_tags");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Community).HasColumnName("community");
            entity.Property(e => e.Tag).HasColumnName("tag");

            entity.HasOne(d => d.CommunityEntityNavigation).WithMany(p => p.CommunityTags)
                .HasForeignKey(d => d.Community)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("community_tags_communities_id_fk");

            entity.HasOne(d => d.TagEntityNavigation).WithMany(p => p.CommunityTags)
                .HasForeignKey(d => d.Tag)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("community_tags_tags_id_fk");
        });

        modelBuilder.Entity<FollowEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("follows_pk");

            entity.ToTable("follows");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Community).HasColumnName("community");
            entity.Property(e => e.Follower).HasColumnName("follower");

            entity.HasOne(d => d.CommunityEntityNavigation).WithMany(p => p.Follows)
                .HasForeignKey(d => d.Community)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("follows_community_fkey");

            entity.HasOne(d => d.FollowerNavigation).WithMany(p => p.Follows)
                .HasForeignKey(d => d.Follower)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("follows_follower_fkey");
        });

        modelBuilder.Entity<JoinRequestEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("join_requests_pk");

            entity.ToTable("join_requests");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Community).HasColumnName("community");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.CommunityEntityNavigation).WithMany(p => p.JoinRequests)
                .HasForeignKey(d => d.Community)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("join_requests_communities_id_fk");

            entity.HasOne(d => d.UserEntityNavigation).WithMany(p => p.JoinRequests)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("join_requests_users_id_fk");
        });

        modelBuilder.Entity<ModeratorEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("moderators_pk");

            entity.ToTable("moderators");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Community).HasColumnName("community");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.CommunityEntityNavigation).WithMany(p => p.Moderators)
                .HasForeignKey(d => d.Community)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moderators_communities_id_fk");

            entity.HasOne(d => d.UserEntityNavigation).WithMany(p => p.Moderators)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moderators_users_id_fk");
        });

        modelBuilder.Entity<PostEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("posts_pkey");

            entity.ToTable("posts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Community).HasColumnName("community");
            entity.Property(e => e.ContentText)
                .HasColumnType("character varying")
                .HasColumnName("content_text");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DislikesCount)
                .HasDefaultValue(0)
                .HasColumnName("dislikes_count");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LikesCount)
                .HasDefaultValue(0)
                .HasColumnName("likes_count");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.CommunityEntityNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Community)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posts_communities_id_fk");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("posts_created_by_fkey");
        });

        modelBuilder.Entity<PostContentMediaEntity>(entity =>
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

        modelBuilder.Entity<PostSubcategoryEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_subcategories_pk");

            entity.ToTable("post_subcategories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.Subcategory).HasColumnName("subcategory");

            entity.HasOne(d => d.PostEntityNavigation).WithMany(p => p.PostSubcategories)
                .HasForeignKey(d => d.Post)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_subcategories_posts_id_fk");

            entity.HasOne(d => d.SubcategoryEntityNavigation).WithMany(p => p.PostSubcategories)
                .HasForeignKey(d => d.Subcategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_subcategories_subcategories_id_fk");
        });

        modelBuilder.Entity<PostTagEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("post_tags_pk");

            entity.ToTable("post_tags");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.Tag).HasColumnName("tag");

            entity.HasOne(d => d.PostEntityNavigation).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.Post)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_tags_posts_id_fk");

            entity.HasOne(d => d.TagEntityNavigation).WithMany(p => p.PostTags)
                .HasForeignKey(d => d.Tag)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("post_tags_tags_id_fk");
        });

        modelBuilder.Entity<ReactionEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reactions_pk");

            entity.ToTable("reactions");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Post).HasColumnName("post");
            entity.Property(e => e.User).HasColumnName("user");

            entity.HasOne(d => d.CommentNavigation).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.Comment)
                .HasConstraintName("reactions_comments_id_fk");

            entity.HasOne(d => d.PostNavigation).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.Post)
                .HasConstraintName("reactions_posts_id_fk");

            entity.HasOne(d => d.UserEntityNavigation).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reactions_users_id_fk");
        });

        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
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

        modelBuilder.Entity<TagEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tags_pk");

            entity.ToTable("tags");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Color)
                .HasColumnType("character varying")
                .HasColumnName("color");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
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
            entity.Property(e => e.Role)
                .HasDefaultValue(3)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");

            entity.HasOne(d => d.RoleEntityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_roles_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

