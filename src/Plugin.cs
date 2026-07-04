// Copyright (c) 2026 moon11yy
// SPDX-License-Identifier: MIT

using BepInEx;
using HarmonyLib;
using LootSearchSpeed.Utils;
using LootSearchSpeed.Core;


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

            Log.Init(Logger);
            ModConfig.Init(Config);

            Harmony = new Harmony(PluginInfo.Guid);
            PatchManager.Apply(Harmony);

            Log.Info($"{PluginInfo.Name} v{PluginInfo.Version} loaded.");
        }

    private void OnDestroy()
    {
        Harmony?.UnpatchSelf();

        Log.Info($"{PluginInfo.Name} unloaded.");
    }
}