namespace CobrasAmmoStack;

public sealed record AmmoStackConfig
{
    public bool Enabled { get; init; } = true;

    public int DefaultAmmoStackSize { get; init; } = 500;

    public Dictionary<string, int> Calibers { get; init; } =
        new(StringComparer.OrdinalIgnoreCase);

    public Dictionary<string, int> CustomCalibers { get; init; } =
        new(StringComparer.OrdinalIgnoreCase);

    public Dictionary<string, int> ItemOverrides { get; init; } =
        new(StringComparer.OrdinalIgnoreCase);

    public bool ShowChangedAmmoInServerLog { get; init; } = false;

    public bool ShowUnknownCalibersInServerLog { get; init; } = true;
}