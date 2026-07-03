using BepInEx.Configuration;

namespace LootSearchSpeed;

internal static class ModConfig
{
    internal static ConfigEntry<float> SearchSpeedMultiplier = null!;
    internal static ConfigEntry<float> ItemExamineTimeMultiplier = null!;

    internal static void Init(ConfigFile config)
    {
        SearchSpeedMultiplier = config.Bind(
            "General",
            "SearchSpeedMultiplier",
            0.5f,
            "Multiplier for container/corpse search time. 1.0 = vanilla, 0.5 = 2x faster, 0.25 = 4x faster.");

        ItemExamineTimeMultiplier = config.Bind(
            "General",
            "ItemExamineTimeMultiplier",
            0.5f,
            "Multiplier for item examine/reveal time. 1.0 = vanilla, 0.5 = 2x faster, 0.25 = 4x faster.");
    }
}