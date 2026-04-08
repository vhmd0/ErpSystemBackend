using Domain.Enums;

namespace Domain.ValueObjects;

public sealed record Money
{
    public decimal Amount { get; }
    public MoneyCurrency Currency { get; } // Make it public for EF/JSON

    public Money(decimal amount, MoneyCurrency currency)
    {
        if (amount < 0)
            throw new ArgumentException(
                "Amount cannot be negative for standard Money. Use a Debit/Credit construct instead.");

        Amount = amount;
        Currency = currency;
    }

    public static Money FromString(decimal amount, string currencyCode)
    {
        if (!Enum.TryParse<MoneyCurrency>(currencyCode, true, out var currency))
            throw new ArgumentException($"Currency '{currencyCode}' is not supported.");

        return new Money(amount, currency);
    }


    public string CurrencyCode => Currency.ToString();

    public override string ToString() => $"{Amount:N2} {CurrencyCode}";


    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Currency mismatch: {Currency} vs {other.Currency}");
        return new Money(Amount + other.Amount, Currency);
    }
}