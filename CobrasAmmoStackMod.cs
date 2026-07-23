using System.Reflection;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;

namespace CobrasAmmoStack;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 100)]
public sealed class CobrasAmmoStackMod(
    ISptLogger<CobrasAmmoStackMod> logger,
    DatabaseService databaseService,
    ModHelper modHelper) : IOnLoad
{
    private const string ModName = "Cobra's Ammo Stack";
    private const string ModVersion = "1.0.0";
    private const string SupportedSptVersion = "4.0.13";

    public Task OnLoad()
    {
        var modFolder = modHelper.GetAbsolutePathToModFolder(
            Assembly.GetExecutingAssembly());

        var config = modHelper.GetJsonDataFromFile<AmmoStackConfig>(
            modFolder,
            "config.json");

        if (!config.Enabled)
        {
            logger.Warning(
                $"[{ModName}] Disabled in config.json.");

            return Task.CompletedTask;
        }

        var tables = databaseService.GetTables();
        var items = tables.Templates.Items;

        var changedItems = 0;

        var unknownCalibers = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        foreach (var item in items.Values)
        {
            var properties = item.Properties;

            if (properties?.Caliber is null)
            {
                continue;
            }

            var caliber = properties.Caliber;

            var stackSize = ResolveStackSize(
                item.Id,
                caliber,
                config,
                unknownCalibers);

            if (stackSize < 1)
            {
                logger.Warning(
                    $"[{ModName}] Invalid stack size {stackSize} " +
                    $"for item {item.Id}. Using 1 instead.");

                stackSize = 1;
            }

            properties.StackMaxSize = stackSize;
            changedItems++;

            if (config.ShowChangedAmmoInServerLog)
            {
                logger.Info(
                    $"[{ModName}] Item={item.Id}, " +
                    $"Caliber={caliber}, Stack={stackSize}");
            }
        }

        ShowStartupSummary(config, changedItems);

        if (config.ShowUnknownCalibersInServerLog
            && unknownCalibers.Count > 0)
        {
            logger.Warning(
                $"[{ModName}] The following calibers were not found " +
                "in Calibers or CustomCalibers and used " +
                $"DefaultAmmoStackSize ({config.DefaultAmmoStackSize}):");

            foreach (var caliber in unknownCalibers.OrderBy(x => x))
            {
                logger.Warning(
                    $"[{ModName}] Unknown caliber: {caliber}");
            }
        }

        return Task.CompletedTask;
    }

    private void ShowStartupSummary(
    AmmoStackConfig config,
    int changedItems)
{
    logger.Info("============================================================");
    logger.Success("                 Cobra's Ammo Stack");
    logger.Info($"                      Version {ModVersion}");
    logger.Info($"                 Built for SPT {SupportedSptVersion}");
    logger.Info("------------------------------------------------------------");

    logger.Success("[OK] Loaded Successfully");
    logger.Info($"[OK] Ammo Modified      : {changedItems}");
    logger.Info($"[OK] Default Stack Size : {config.DefaultAmmoStackSize}");
    logger.Info($"[OK] Known Calibers     : {config.Calibers.Count}");
    logger.Info($"[OK] Custom Calibers    : {config.CustomCalibers.Count}");
    logger.Info($"[OK] Item Overrides     : {config.ItemOverrides.Count}");

    logger.Info("============================================================");
}

    private static int ResolveStackSize(
        string itemId,
        string caliber,
        AmmoStackConfig config,
        HashSet<string> unknownCalibers)
    {
        // Highest priority: exact item-template override.
        if (config.ItemOverrides.TryGetValue(
                itemId,
                out var itemStackSize))
        {
            return itemStackSize;
        }

        // Standard caliber configuration.
        if (config.Calibers.TryGetValue(
                caliber,
                out var caliberStackSize))
        {
            return caliberStackSize;
        }

        // Custom calibers added by other mods.
        if (config.CustomCalibers.TryGetValue(
                caliber,
                out var customStackSize))
        {
            return customStackSize;
        }

        // Unknown ammunition uses the configured default.
        unknownCalibers.Add(caliber);

        return config.DefaultAmmoStackSize;
    }
}