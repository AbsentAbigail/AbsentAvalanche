using System.Linq;
using AbsentUtilities;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using WildfrostHopeMod.Utils;

namespace AbsentAvalanche.Patches;

[HarmonyPatch(typeof(PetHutFlagSetter), "SetupFlag")]
internal static class PetHutFlagSetterPatches
{
    private static readonly string[] Flags =
    [
        "Flag_Catcus",
        "Flag_SalvoKitty",
        "Flag_NebulaAuxilium"
    ];

    [UsedImplicitly]
    private static void Prefix(PetHutFlagSetter __instance)
    {
        foreach (var flag in Flags)
        {
            var texture = AbsentUtils.GetModInfo().Sprites.GetSprite(flag).ToTexture();
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 1f),
                160, 0U, SpriteMeshType.FullRect);
            __instance.flagSprites = __instance.flagSprites.Append(sprite).ToArray();
        }
    }
}