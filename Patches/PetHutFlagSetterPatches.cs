﻿using System.Linq;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(PetHutFlagSetter), "SetupFlag")]
internal static class PetHutFlagSetterPatches
{
    private static readonly string[] Flags =
    [
        "Flag_Catcus.png",
        "Flag_SalvoKitty.png",
        "Flag_NebulaAuxilium.png"
    ];

    [UsedImplicitly]
    private static void Prefix(PetHutFlagSetter __instance)
    {
        foreach (var flag in Flags)
        {
            var texture = Absent.Instance.ImagePath(flag).ToTex();
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 1f),
                160);
            __instance.flagSprites = __instance.flagSprites.Append(sprite).ToArray();
        }
    }
}