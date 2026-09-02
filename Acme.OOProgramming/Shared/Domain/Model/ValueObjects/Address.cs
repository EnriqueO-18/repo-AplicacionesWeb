namespace Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

/// <summary>
/// Represents a physical postal address.
/// </summary>
/// <remarks>
/// This type is a Value Object. Its identity is determined by the combination
/// of its address components rather than by a unique identifier.
/// </remarks>
public readonly record struct Address
{
    /// <summary>
    /// Gets the street name.
    /// </summary>
    public string Street { get; }

    /// <summary>
    /// Gets the street or building number.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Gets the city name.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Gets the state, province, or region.
    /// </summary>
    public string? StateOrRegion { get; }

    /// <summary>
    /// Gets the postal or ZIP code.
    /// </summary>
    public string PostalCode { get; }

    /// <summary>
    /// Gets the country name.
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> value object.
    /// </summary>
    /// <param name="street">The street name. Maximum length is 100 characters.</param>
    /// <param name="number">The street or building number. Maximum length is 10 characters.</param>
    /// <param name="city">The city name. Maximum length is 100 characters.</param>
    /// <param name="stateOrRegion">
    /// The optional state, province, or region. Maximum length is 100 characters.
    /// </param>
    /// <param name="postalCode">The postal or ZIP code. Maximum length is 20 characters.</param>
    /// <param name="country">The country name. Maximum length is 100 characters.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when a required value is null, empty, consists only of whitespace,
    /// or exceeds its maximum allowed length.
    /// </exception>
    public Address(
        string street,
        string number,
        string city,
        string? stateOrRegion,
        string postalCode,
        string country)
    {
        Street = ValidateRequired(street, nameof(street), 100);
        Number = ValidateRequired(number, nameof(number), 10);
        City = ValidateRequired(city, nameof(city), 100);
        StateOrRegion = ValidateOptional(stateOrRegion, nameof(stateOrRegion), 100);
        PostalCode = ValidateRequired(postalCode, nameof(postalCode), 20);
        Country = ValidateRequired(country, nameof(country), 100);
    }

    /// <summary>
    /// Returns a string representation of the address.
    /// </summary>
    /// <returns>
    /// A formatted address containing the street, number, city, postal code,
    /// country, and state or region when provided.
    /// </returns>
    public override string ToString()
    {
        return string.IsNullOrWhiteSpace(StateOrRegion)
            ? $"{Street} {Number}, {City}, {PostalCode}, {Country}"
            : $"{Street} {Number}, {City}, {StateOrRegion}, {PostalCode}, {Country}";
    }

    /// <summary>
    /// Validates a required string value.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="parameterName">The parameter name used in the exception message.</param>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <returns>The validated value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the value is null, empty, consists only of whitespace,
    /// or exceeds the maximum allowed length.
    /// </exception>
    private static string ValidateRequired(
        string value,
        string parameterName,
        int maxLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        if (value.Length > maxLength)
        {
            throw new ArgumentException(
                $"{parameterName} cannot exceed {maxLength} characters.",
                parameterName);
        }

        return value;
    }

    /// <summary>
    /// Validates an optional string value.
    /// </summary>
    /// <param name="value">The optional value to validate.</param>
    /// <param name="parameterName">The parameter name used in the exception message.</param>
    /// <param name="maxLength">The maximum allowed length.</param>
    /// <returns>
    /// The validated value, or <see langword="null"/> when the value is null,
    /// empty, or consists only of whitespace.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the value exceeds the maximum allowed length.
    /// </exception>
    private static string? ValidateOptional(
        string? value,
        string parameterName,
        int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException(
                $"{parameterName} cannot exceed {maxLength} characters.",
                parameterName);
        }

        return value;
    }
}
