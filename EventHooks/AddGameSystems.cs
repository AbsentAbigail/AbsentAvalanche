using AbsentAvalanche.GameSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AbsentAvalanche.EventHooks;

public static class AddGameSystems
{
    public static void SceneLoaded(Scene scene)
    {
        if (scene.name != "Campaign")
        {
            return;
        }

        GameObject.Find("Systems")?.AddComponent<ChargeRedrawBellSystem>();
        GameObject.Find("Systems")?.AddComponent<CustomTextPopupSystem>();
    }
}