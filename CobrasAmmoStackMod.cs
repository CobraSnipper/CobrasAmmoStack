using System.Reflection;
using System.Text.Json;
using SPTarkov.Common.Models.Logging;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Tables;
using SPTarkov.Server.Core.Models.Utils;

namespace CobrasAmmoStack;


[Injectable]
public sealed class CobrasAmmoStackMod(
    ISptLogger<CobrasAmmoStackMod> logger,
    TemplateTable templateTable) : IOnLoad
{
    private const string ModName = "Cobra's Ammo Stack";
    private const string ModVersion = "1.1.0";

    public Task OnLoadAsync(CancellationToken cancellationToken)
    {
        try
        {
            var modFolder = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            if (string.IsNullOrWhiteSpace(modFolder))
            {
                logger.Error($"[{ModName}] Could not locate the mod folder.");
                return Task.CompletedTask;
            }

            var configPath = Path.Combine(modFolder, "config.json");

            if (!File.Exists(configPath))
            {
                logger.Error($"[{ModName}] config.json was not found.");
                return Task.CompletedTask;
            }

            var config = JsonSerializer.Deserialize<AmmoStackConfig>(
                File.ReadAllText(configPath),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (config is null)
            {
                logger.Error($"[{ModName}] Could not read config.json.");
                return Task.CompletedTask;
            }

            if (!config.Enabled)
            {
                logger.Warning($"[{ModName}] Disabled in config.json.");
                return Task.CompletedTask;
            }

            
            var changedCount = 0;

            foreach (var itemEntry in templateTable.Items)
            {
                var itemId = itemEntry.Key;
                var item = itemEntry.Value;

                var caliber = item.Properties?.Caliber;

                if (string.IsNullOrWhiteSpace(caliber))
                {
                    continue;
                }

                var stackSize = config.DefaultAmmoStackSize;

                if (config.Calibers.TryGetValue(caliber, out var configuredSize))
                {
                    stackSize = configuredSize;
                }
                else if (config.CustomCalibers.TryGetValue(caliber, out var customSize))
                {
                    stackSize = customSize;
                }

                if (config.ItemOverrides.TryGetValue(itemId, out var overrideSize))
                {
                    stackSize = overrideSize;
                }

                item.Properties.StackMaxSize = stackSize;
                changedCount++;
            }

            logger.Success(
                $"[{ModName}] v{ModVersion} loaded. Modified {changedCount} ammunition items.");
        }
        catch (JsonException)
        {
            logger.Error($"[{ModName}] config.json contains invalid JSON.");
        }
        catch (Exception exception)
        {
            logger.Error($"[{ModName}] Failed to load: {exception.Message}");
        }

        return Task.CompletedTask;
    }
}