#region

using UnityEngine;

#endregion

namespace AbsentAvalanche.Helpers;

public static class LogHelper
{
    public static void Log(object message)
    {
        Debug.Log($"[AbsentAvalanche] {message}");
    }

    public static void Warn(object message)
    {
        Debug.LogWarning($"[AbsentAvalanche Warning] {message}");
    }

    public static void Error(object message)
    {
        Debug.LogError($"[AbsentAvalanche Error] {message}");
    }
}