// ReSharper disable All
namespace ConverseSpace.Data.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<SubcategoryEntity> Subcategories { get; set; } = new List<SubcategoryEntity>();
}