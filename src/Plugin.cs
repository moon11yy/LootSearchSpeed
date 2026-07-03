using BepInEx;
using HarmonyLib;
using LootSearchSpeed.Utils;

namespace LootSearchSpeed;

[BepInPlugin(
    PluginInfo.Guid,
    PluginInfo.Name,
    PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
    internal static Plugin Instance { get; private set; } = null!;

    internal static Harmony Harmony { get; private set; } = null!;

    private void Awake()
    {
        Instance = this;

        ModConfig.Init(Config);

        Harmony = new Harmony(PluginInfo.Guid);
        Harmony.PatchAll();

        Log.Info($"{PluginInfo.Name} v{PluginInfo.Version} loaded.");
    }

    private void OnDestroy()
    {
        Harmony?.UnpatchSelf();

        Log.Info($"{PluginInfo.Name} unloaded.");
    }
}