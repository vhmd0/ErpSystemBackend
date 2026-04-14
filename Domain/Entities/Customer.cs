using Domain.ValueObjects;

namespace Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Address ShippingAddress { get; set; } = default!; // Using the Address Value Object
}
