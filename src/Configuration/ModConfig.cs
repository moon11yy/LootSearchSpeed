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
                "Controls the initial 'Searching...' delay before a container, corpse or other searchable loot opens. 1.0 = vanilla, 0.5 = 2x faster, 0.0 = instant.",
                new AcceptableValueRange<float>(0f, 10f)));

        ItemRevealDelayMultiplier = config.Bind(
            "Search Speed",
            "ItemRevealDelayMultiplier",
            0.5f,
            new ConfigDescription(
                "Controls how quickly items are revealed one by one inside containers, corpses, bags, jackets and similar searchable loot. 1.0 = vanilla, 0.5 = 2x faster, 0.0 = instant.",
                new AcceptableValueRange<float>(0f, 10f)));
    }
}