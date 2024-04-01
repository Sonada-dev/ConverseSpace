namespace ConverseSpace.Data.Entities;

public class RoleEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}