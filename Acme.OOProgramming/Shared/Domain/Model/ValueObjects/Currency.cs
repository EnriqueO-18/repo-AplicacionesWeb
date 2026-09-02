namespace Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a currency value object using an ISO 4217 alphabetic code.
/// </summary>
public readonly record struct Currency
{
    /// <summary>
    /// Gets or initializes the three-letter ISO 4217 alphabetic currency code.
    /// </summary>
    /// <remarks>
    /// The currency code must contain exactly three ASCII letters.
    /// The value is normalized to uppercase.
    /// </remarks>
    public string Code
    {
        get => field ?? string.Empty;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            if (value.Length != 3 || !value.All(char.IsAsciiLetter))
                throw new ArgumentException(
                    "Currency must be a 3-letter ISO 4217 alphabetic code.",
                    nameof(value));

            field = value.ToUpperInvariant();
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Currency"/> type.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Always thrown because a <see cref="Currency"/> must be initialized
    /// with a valid currency code.
    /// </exception>
    public Currency() =>
        throw new InvalidOperationException(
            "Currency must be initialized with a valid ISO 4217 alphabetic code");

    /// <summary>
    /// Initializes a new instance of the <see cref="Currency"/> type.
    /// </summary>
    /// <param name="code">
    /// The three-letter ISO 4217 alphabetic currency code.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="code"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="code"/> is empty, consists only of whitespace,
    /// or does not contain exactly three ASCII letters.
    /// </exception>
    public Currency(string code) => Code = code;

    /// <summary>
    /// Returns the currency code as a string.
    /// </summary>
    /// <returns>
    /// The three-letter ISO 4217 alphabetic currency code.
    /// </returns>
    public override string ToString() => Code;
}
