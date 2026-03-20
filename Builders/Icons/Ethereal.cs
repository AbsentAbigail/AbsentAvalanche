#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;
using WildfrostHopeMod.VFX;

#endregion

namespace AbsentAvalanche.Builders.Icons;

[UsedImplicitly]
public class Ethereal : IIconBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Absent.Instance)
            .Create(name: Name,
                statusType: "ethereal",
                Absent.Instance.ImagePath("Icons/ethereal.png").ToSprite())
            .WithIconGroupName(StatusIconBuilder.IconGroups.damage)
            .WithTextColour(new Color(0.2f, 0.2f, 0.3f))
            .WithTextShadow(KeywordColours.Rest)
            .WithTextboxSprite()
            .WithKeywords(Keywords.Ethereal.Name);
    }
}