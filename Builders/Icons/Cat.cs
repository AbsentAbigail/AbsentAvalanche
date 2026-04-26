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
public class Cat : IIconBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Absent.Instance)
            .Create(name: Name,
                statusType: "cat",
                Absent.Instance.ImagePath("Icons/cat.png").ToSprite())
            .WithIconGroupName(StatusIconBuilder.IconGroups.damage)
            .WithTextColour(new Color(0.2f, 0.2f, 0.3f))
            .WithTextShadow(KeywordColours.Red)
            // .WithTextboxSprite(Absent.Instance.ImagePath("Icons/catkeyword.png").ToSprite())
            .WithTextboxSprite()
            .WithKeywords(Keywords.Cat.Name);
    }
}