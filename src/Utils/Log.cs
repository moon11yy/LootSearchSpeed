using BepInEx.Logging;

namespace LootSearchSpeed.Utils;

internal static class Log
{
    internal static ManualLogSource Logger { get; private set; } = null!;

    internal static void Init(ManualLogSource logger)
    {
        Logger = logger;
    }

    internal static void Info(string message) => Logger.LogInfo(message);

    internal static void Warning(string message) => Logger.LogWarning(message);

    internal static void Error(string message) => Logger.LogError(message);
}