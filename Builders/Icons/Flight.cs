using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using WildfrostHopeMod.VFX;

namespace AbsentAvalanche.Builders.Icons;

[UsedImplicitly]
public class Flight : IIconBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Absent.Instance)
            .Create(name: Name,
                statusType: "flight",
                Absent.Instance.ImagePath("Icons/flight.png").ToSprite())
            .WithIconGroupName(StatusIconBuilder.IconGroups.health)
            .WithTextColour(new Color(0.2f, 0.2f, 0.3f))
            .WithTextShadow(KeywordColours.Blue)
            .WithTextboxSprite()
            .WithKeywords(Keywords.Flight.Name);
    }
}