namespace Domain.ValueObjects;

public sealed record Sku
{
    public string Value { get; }

    public Sku(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 20)
            throw new ArgumentException("SKU format invalid");
        Value = value.Trim().ToUpperInvariant();
    }

    public static implicit operator string(Sku sku) => sku.Value;
    public static implicit operator Sku(string value) => new(value);
}