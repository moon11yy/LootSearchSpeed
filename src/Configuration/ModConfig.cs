using BepInEx.Configuration;

namespace LootSearchSpeed;

internal static class ModConfig
{
    internal static ConfigEntry<float> InitialSearchDelayMultiplier = null!;
    internal static ConfigEntry<float> ItemRevealDelayMultiplier = null!;

    internal static void Init(ConfigFile config)
    {
        InitialSearchDelayMultiplier = config.Bind(
            "Search Speed",
            "InitialSearchDelayMultiplier",
            0.5f,
            new ConfigDescription(
                "Multiplier for the initial Searching delay. 1.0 = vanilla, 0.5 = 2x faster, 0.0 = instant.",
                new AcceptableValueRange<float>(0f, 10f)));

        ItemRevealDelayMultiplier = config.Bind(
            "Search Speed",
            "ItemRevealDelayMultiplier",
            0.5f,
            new ConfigDescription(
                "Multiplier for item reveal delay inside searched containers. 1.0 = vanilla, 0.5 = 2x faster, 0.0 = instant.",
                new AcceptableValueRange<float>(0f, 10f)));
    }
}