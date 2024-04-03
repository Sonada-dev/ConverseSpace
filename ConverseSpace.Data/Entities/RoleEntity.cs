﻿namespace ConverseSpace.Data.Entities;

public class RoleEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}