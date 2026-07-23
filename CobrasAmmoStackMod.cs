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
    private const string ModVersion = "1.0.1";

    public Task OnLoad()
    {
        var modFolder = modHelper.GetAbsolutePathToModFolder(
            Assembly.GetExecutingAssembly());

        var config = modHelper.GetJsonDataFromFile<AmmoStackConfig>(
            modFolder,
            "config.json");

        if (!config.Enabled)
        {
            logger.Warning($"[{ModName}] Disabled in config.json.");
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

        logger.Success(
            $"[{ModName}] v{ModVersion} loaded. " +
            $"Modified {changedItems} ammunition items.");

        if (config.ShowUnknownCalibersInServerLog
            && unknownCalibers.Count > 0)
        {
            logger.Warning(
                $"[{ModName}] {unknownCalibers.Count} unknown caliber(s) " +
                $"used DefaultAmmoStackSize ({config.DefaultAmmoStackSize}).");

            foreach (var caliber in unknownCalibers.OrderBy(x => x))
            {
                logger.Warning(
                    $"[{ModName}] Unknown caliber: {caliber}");
            }
        }

        return Task.CompletedTask;
    }

    private static int ResolveStackSize(
        string itemId,
        string caliber,
        AmmoStackConfig config,
        HashSet<string> unknownCalibers)
    {
        if (config.ItemOverrides.TryGetValue(
                itemId,
                out var itemStackSize))
        {
            return itemStackSize;
        }

        if (config.Calibers.TryGetValue(
                caliber,
                out var caliberStackSize))
        {
            return caliberStackSize;
        }

        if (config.CustomCalibers.TryGetValue(
                caliber,
                out var customStackSize))
        {
            return customStackSize;
        }

        unknownCalibers.Add(caliber);

        return config.DefaultAmmoStackSize;
    }
}