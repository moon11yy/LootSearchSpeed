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

        Assembly assembly = typeof(EFT.Player).Assembly;

        var types = assembly
            .GetTypes()
            .Where(type =>
                type.Name.IndexOf("Search", StringComparison.OrdinalIgnoreCase) >= 0 ||
                (type.FullName != null && type.FullName.IndexOf("Search", StringComparison.OrdinalIgnoreCase) >= 0))
            .OrderBy(type => type.FullName)
            .ToList();

        Log.Info($"Found {types.Count} types containing 'Search'.");

        foreach (var type in types.Take(50))
        {
            Log.Info($"[SearchDiscovery] Type: {type.FullName}");

            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly);

            foreach (MethodInfo method in methods)
            {
                Log.Info($"[SearchDiscovery]   Method: {method.Name} -> {method.ReturnType.Name}");
            }
        }

        Log.Info("Search discovery finished.");
    }
}