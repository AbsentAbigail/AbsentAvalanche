#region

using AbsentAvalanche.Builders.Interfaces;
using AbsentAvalanche.Builders.Keywords;
using Deadpan.Enums.Engine.Components.Modding;
using HarmonyLib;
using JetBrains.Annotations;
using WildfrostHopeMod.VFX;

#endregion

namespace AbsentAvalanche.Builders.Icons;

[UsedImplicitly]
public class Calm : IIconBuilder
{
    public static string Name { get; } = AccessTools.GetOutsideCaller().DeclaringType!.Name.ToLower();

    public DataFileBuilder<_StatusIconData, StatusIconBuilder> Builder()
    {
        return new StatusIconBuilder(Absent.Instance)
            .Create(name: Name,
                statusType: "calm",
                Absent.Instance.ImagePath("Icons/calm.png").ToSprite())
            .WithIconGroupName(StatusIconBuilder.IconGroups.counter)
            .WithTextColour(KeywordColours.White)
            .WithTextShadow(KeywordColours.White)
            .WithTextboxSprite()
            .WithKeywords(Keywords.Calm.Name);
    }
}