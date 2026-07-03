using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using LootSearchSpeed.Core;
using LootSearchSpeed.Utils;

namespace LootSearchSpeed.Patches;

internal sealed class ContainerSearchPatch : BasePatch
{
    public ContainerSearchPatch(Harmony harmony) : base(harmony) { }

    internal override void Apply()
    {
        Type? initialStateMachine = AccessTools.TypeByName("GClass3515+Struct915");
        Type? revealStateMachine = AccessTools.TypeByName("GClass3515+Struct916");

        if (initialStateMachine == null || revealStateMachine == null)
        {
            Log.Warning("Container search state machines were not found.");
            return;
        }

        MethodInfo? initialMoveNext = AccessTools.Method(initialStateMachine, "MoveNext");
        MethodInfo? revealMoveNext = AccessTools.Method(revealStateMachine, "MoveNext");

        if (initialMoveNext == null || revealMoveNext == null)
        {
            Log.Warning("Container search MoveNext methods were not found.");
            return;
        }

        Harmony.Patch(
            initialMoveNext,
            transpiler: new HarmonyMethod(typeof(ContainerSearchPatch), nameof(InitialDelayTranspiler)));

        Harmony.Patch(
            revealMoveNext,
            transpiler: new HarmonyMethod(typeof(ContainerSearchPatch), nameof(ItemRevealDelayTranspiler)));

        Log.Info("ContainerSearchPatch applied.");
    }

    private static IEnumerable<CodeInstruction> InitialDelayTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (CodeInstruction instruction in instructions)
        {
            if (instruction.opcode == OpCodes.Ldc_I4 && instruction.operand is int value && value == 2000)
            {
                yield return new CodeInstruction(
                    OpCodes.Call,
                    AccessTools.Method(typeof(ContainerSearchPatch), nameof(GetInitialSearchDelayMs)));

                continue;
            }

            yield return instruction;
        }
    }

    private static IEnumerable<CodeInstruction> ItemRevealDelayTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        foreach (CodeInstruction instruction in instructions)
        {
            if (instruction.opcode == OpCodes.Ldc_R4 && instruction.operand is float value && Math.Abs(value - 1000f) < 0.01f)
            {
                yield return new CodeInstruction(
                    OpCodes.Call,
                    AccessTools.Method(typeof(ContainerSearchPatch), nameof(GetItemRevealDelayBaseMs)));

                continue;
            }

            yield return instruction;
        }
    }

    private static int GetInitialSearchDelayMs()
    {
        float multiplier = ModConfig.InitialSearchDelayMultiplier.Value;
        return Math.Max(0, (int)(2000f * multiplier));
    }

    private static float GetItemRevealDelayBaseMs()
    {
        float multiplier = ModConfig.ItemRevealDelayMultiplier.Value;
        return Math.Max(0f, 1000f * multiplier);
    }
}