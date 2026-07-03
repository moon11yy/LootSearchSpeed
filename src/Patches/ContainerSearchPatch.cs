using System;
using System.Reflection;
using HarmonyLib;
using LootSearchSpeed.Core;
using LootSearchSpeed.Utils;

namespace LootSearchSpeed.Patches;

internal sealed class ContainerSearchPatch : BasePatch
{
    public ContainerSearchPatch(Harmony harmony) : base(harmony)
    {
    }

    internal override void Apply()
    {
        Type? targetType = AccessTools.TypeByName("GClass3515");

        if (targetType == null)
        {
            Log.Warning("Container search class GClass3515 was not found.");
            return;
        }

        MethodInfo? method5 = AccessTools.Method(targetType, "method_5");
        MethodInfo? method6 = AccessTools.Method(targetType, "method_6");

        if (method5 == null)
        {
            Log.Warning("Container search initial delay method_5 was not found.");
        }

        if (method6 == null)
        {
            Log.Warning("Container search item reveal method_6 was not found.");
        }

        Log.Info("ContainerSearchPatch discovery complete.");
    }
}