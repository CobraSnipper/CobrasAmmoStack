using SPTarkov.Server.Core.Models.Spt.Mod;

namespace CobrasAmmoStack;

public sealed record ModMetadata : IModMetadata
{
    public string ModGuid { get; init; } =
        "com.cobra.ammostack";

    public string Name { get; init; } =
        "Cobra's Ammo Stack";

    public string Author { get; init; } =
        "Cobra";

    public List<string>? Contributors { get; init; } =
        null;

    public SemanticVersioning.Version Version { get; init; } =
        new("1.1.0");

    public SemanticVersioning.Range SptVersion { get; init; } =
        new("~4.1.0");

    public List<string>? Incompatibilities { get; init; } =
        null;

    public Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } =
        null;

    public string? Url { get; init; } =
        "https://github.com/CobraSnipper/CobrasAmmoStack";

    public string License { get; init; } =
        "MIT";

    public bool HasPrepatcher { get; init; } =
        false;
}