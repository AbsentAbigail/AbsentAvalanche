using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.Builders.Icons;

[UsedImplicitly]
public class FreePlay : IIconBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Absent.Instance)
            .Create(name: Name,
                statusType: "freeplay",
                Absent.Instance.ImagePath("Icons/freeplay.png").ToSprite())
            .WithIconGroupName(StatusIconBuilder.IconGroups.crown)
            .WithTextColour(new Color(0.2f, 0.2f, 0.3f))
            .WithTextShadow(KeywordColours.Blue)
            .WithTextboxSprite()
            .WithKeywords(Keywords.FreePlay.Name);
    }
}