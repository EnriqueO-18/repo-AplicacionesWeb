namespace Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a monetary value with an amount and currency.
/// </summary>
public readonly record struct Money
{
    /// <summary>
    /// Gets or initializes the monetary amount.
    /// </summary>
    public decimal Amount
    {
        get;
        init
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value);
            field = value;
        }
    }

    /// <summary>
    /// Gets or initializes the currency.
    /// </summary>
    public Currency Currency
    {
        get;
        init
        {
            if (value == default)
                throw new InvalidOperationException("Currency is required.");

            field = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> type.
    /// </summary>
    public Money() =>
        throw new InvalidOperationException(
            "Money must be initialized with a valid amount and currency.");

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> type.
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currency">The currency of the monetary value.</param>
    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Money"/> type.
    /// </summary>
    /// <param name="amount">The monetary amount.</param>
    /// <param name="currencyCode">The ISO 4217 currency code.</param>
    public Money(decimal amount, string currencyCode)
        : this(amount, new Currency(currencyCode))
    {
    }

    /// <summary>
    /// Returns the monetary value as a string.
    /// </summary>
    /// <returns>The amount followed by the currency code.</returns>
    public override string ToString() => $"{Amount} {Currency}";

    /// <summary>
    /// Adds another monetary value with the same currency.
    /// </summary>
    /// <param name="other">The monetary value to add.</param>
    /// <returns>A new <see cref="Money"/> containing the sum.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when either currency is not initialized or the currencies differ.
    /// </exception>
    public Money Add(Money other)
    {
        if (Currency == default || other.Currency == default)
            throw new InvalidOperationException(
                "Cannot add Money with an uninitialized currency.");

        if (Currency != other.Currency)
            throw new InvalidOperationException(
                $"Cannot add Money with different currencies: {Currency} and {other.Currency}.");

        return new Money(Amount + other.Amount, Currency);
    }

    /// <summary>
    /// Multiplies the monetary amount by a non-negative factor.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>A new <see cref="Money"/> containing the multiplied amount.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the currency is not initialized.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the factor is negative.
    /// </exception>
    public Money Multiply(decimal factor)
    {
        if (Currency == default)
            throw new InvalidOperationException(
                "Cannot multiply Money with an uninitialized currency.");

        ArgumentOutOfRangeException.ThrowIfNegative(factor);

        return new Money(Amount * factor, Currency);
    }

    /// <summary>
    /// Multiplies the monetary amount by a non-negative integer factor.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>A new <see cref="Money"/> containing the multiplied amount.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the currency is not initialized.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the factor is negative.
    /// </exception>
    public Money Multiply(int factor) => Multiply((decimal)factor);

    /// <summary>
    /// Adds two monetary values with the same currency.
    /// </summary>
    /// <param name="left">The first monetary value.</param>
    /// <param name="right">The second monetary value.</param>
    /// <returns>The sum of the two monetary values.</returns>
    public static Money operator +(Money left, Money right) => left.Add(right);

    /// <summary>
    /// Multiplies a monetary value by a decimal factor.
    /// </summary>
    /// <param name="money">The monetary value.</param>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(Money money, decimal factor) =>
        money.Multiply(factor);

    /// <summary>
    /// Multiplies a decimal factor by a monetary value.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <param name="money">The monetary value.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(decimal factor, Money money) =>
        money.Multiply(factor);

    /// <summary>
    /// Multiplies a monetary value by an integer factor.
    /// </summary>
    /// <param name="money">The monetary value.</param>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(Money money, int factor) =>
        money.Multiply(factor);

    /// <summary>
    /// Multiplies an integer factor by a monetary value.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <param name="money">The monetary value.</param>
    /// <returns>The multiplied monetary value.</returns>
    public static Money operator *(int factor, Money money) =>
        money.Multiply(factor);
}
