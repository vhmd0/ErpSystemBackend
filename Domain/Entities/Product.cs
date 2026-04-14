using Domain.ValueObjects;

namespace Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Sku ProductSku { get; set; } = default!;
    public Money Price { get; set; } = default!;
}
