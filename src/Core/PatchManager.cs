using HarmonyLib;
using LootSearchSpeed.Patches;
using LootSearchSpeed.Discovery;

namespace LootSearchSpeed.Core;

internal static class PatchManager
{
    internal static void Apply(Harmony harmony)
    {
//        SearchDiscovery.Run();
//
        new ContainerSearchPatch(harmony).Apply();
        new ItemExaminePatch(harmony).Apply();
    }
}