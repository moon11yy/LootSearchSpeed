using HarmonyLib;

namespace LootSearchSpeed.Patches;

internal abstract class BasePatch
{
    protected readonly Harmony Harmony;

    protected BasePatch(Harmony harmony)
    {
        Harmony = harmony;
    }

    internal abstract void Apply();
}