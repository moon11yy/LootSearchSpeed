using System;
using System.Linq;
using System.Reflection;
using LootSearchSpeed.Utils;

namespace LootSearchSpeed.Discovery;

internal static class SearchDiscovery
{
    internal static void Run()
    {
        Log.Info("Search discovery started.");

        var assembly = typeof(EFT.Player).Assembly;

        var types = assembly
            .GetTypes()
            .Where(type =>
                type.Name.Contains("Search", StringComparison.OrdinalIgnoreCase) ||
                type.FullName?.Contains("Search", StringComparison.OrdinalIgnoreCase) == true)
            .OrderBy(type => type.FullName)
            .ToList();

        Log.Info($"Found {types.Count} types containing 'Search'.");

        foreach (var type in types.Take(50))
        {
            Log.Info($"[SearchDiscovery] Type: {type.FullName}");

            var methods = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly);

            foreach (var method in methods)
            {
                Log.Info($"[SearchDiscovery]   Method: {method.Name} -> {method.ReturnType.Name}");
            }
        }

        Log.Info("Search discovery finished.");
    }
}