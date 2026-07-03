using HarmonyLib;

namespace LootSearchSpeed.Patches;

internal static class PatchManager
{
    internal static void Apply(Harmony harmony)
    {
        harmony.PatchAll();
    }
}