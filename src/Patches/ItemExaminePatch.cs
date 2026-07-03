using HarmonyLib;
using LootSearchSpeed.Core;

namespace LootSearchSpeed.Patches;

internal sealed class ItemExaminePatch : BasePatch
{
    public ItemExaminePatch(Harmony harmony) : base(harmony)
    {
    }

    internal override void Apply()
    {
        // TODO: Implement item examine/reveal speed patch.
    }
}