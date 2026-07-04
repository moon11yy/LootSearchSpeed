// Copyright (c) 2026 moon11yy
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using LootSearchSpeed.Core;
using LootSearchSpeed.Utils;

namespace LootSearchSpeed.Patches;

internal sealed class ContainerSearchPatch : BasePatch
{
    // Default Escape from Tarkov search delays.
    private const int VanillaInitialSearchDelayMs = 2000;
    private const float VanillaItemRevealDelayMs = 1000f;

    public ContainerSearchPatch(Harmony harmony) : base(harmony)
    {
    }

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
            if (instruction.opcode == OpCodes.Ldc_I4 &&
                instruction.operand is int value &&
                value == VanillaInitialSearchDelayMs)
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
            if (instruction.opcode == OpCodes.Ldc_R4 &&
                instruction.operand is float value &&
                Math.Abs(value - VanillaItemRevealDelayMs) < 0.01f)
            {
                yield return new CodeInstruction(
                    OpCodes.Call,
                    AccessTools.Method(typeof(ContainerSearchPatch), nameof(GetItemRevealDelayMs)));

                continue;
            }

            yield return instruction;
        }
    }

    private static int GetInitialSearchDelayMs()
    {
        float multiplier = ModConfig.InitialSearchDelayMultiplier.Value;
        return Math.Max(0, (int)(VanillaInitialSearchDelayMs * multiplier));
    }

    private static float GetItemRevealDelayMs()
    {
        float multiplier = ModConfig.ItemRevealDelayMultiplier.Value;
        return Math.Max(0f, VanillaItemRevealDelayMs * multiplier);
    }
}