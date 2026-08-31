using Acme.OOPProgramming.Shared.Domain.Model.ValueObjects;

namespace Acme.OOPProgramming.Shared.Presentation;

/// <summary>
/// Provides console formatting methods for value objects.
/// </summary>
internal static class ConsoleFormatting
{
    /// <summary>
    /// Formats a Money object for display.
    /// </summary>
    public static string Display(this Money money)
        => $"{money.Amount:N2} {money.Currency.Code}";
}