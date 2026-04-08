namespace Domain.ValueObjects;


public sealed record Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    public Address(string street, string city, string state, string postalCode, string country)
    {
     
        if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street required");
        if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City required");
        if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("State required");
        if (string.IsNullOrWhiteSpace(postalCode)) throw new ArgumentException("Postal code required");
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country required");

        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public override string ToString() => $"{Street}, {City}, {State} {PostalCode}, {Country}";
}