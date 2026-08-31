namespace Acme.OOProgramming.Shared.Domain.Model.ValueObjects;

public readonly record struct Address
{
    public string Street
    {
        get => field ?? string.Empty;
        init
        {
            ArgumentException.ThrowIfNullOrEmpty(value);
            if (value.Length > 100)
                throw new ArgumentException("Address is too long.", nameof (value));
            field = value;
        }
    }
}